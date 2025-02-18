using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WealthWiseAPI.Data;
using WealthWiseAPI.Models;
using BCrypt.Net;
using System.Threading.Tasks;
using WealthWiseAPI.Helpers;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Cors;

namespace WealthWiseAPI.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _jwtHelper = new JwtHelper(config["JwtSettings:SecretKey"]);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest(new { message = "Email already exists" });

            //user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, workFactor: 12);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User registered successfully" });
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] User loginRequest)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
        //    bool hi = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
        //    Console.Write(hi);
        //    if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
        //        return Unauthorized(new { message = "Invalid email or password" });

        //    string token = _jwtHelper.GenerateToken(user.Email, user.Role);
        //    return Ok(new { token });
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
                return Unauthorized(new { message = "Invalid email or password" });

            // Hardcoded Admin Credentials
            if (user.Email == "admin@mail.com" && loginRequest.Password == "admin@123")
            {
                user.Role = "Admin"; // Assign admin role
            }
            else
            {
                user.Role = "User"; // Default role
            }

            string token = _jwtHelper.GenerateToken(user.Email, user.Role);

            return Ok(new
            {
                token,
                role = user.Role,
                id = user.Id// Include role in response 
            });
        }



        //[Authorize]
        //[HttpGet("profile")]
        //public async Task<IActionResult> GetUserProfile()
        //{
        //    string email = User.FindFirst(ClaimTypes.Email)?.Value;
        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        //    return Ok(user);
        //}

        // GET USER PROFILE
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }


        // LOGOUT
        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Logged out successfully" });
        }

        // ENABLE TWO-FACTOR AUTHENTICATION
        [Authorize]
        [HttpPatch("enable-two-factor/verify-otp/{otp}")]
        public async Task<IActionResult> EnableTwoFactorAuth(string otp)
        {
            var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var verificationCode = await _context.VerificationCodes
                .FirstOrDefaultAsync(v => v.UserId == userId && v.OTP == otp && v.VerificationType == "two-factor");

            if (verificationCode == null)
            {
                return BadRequest(new { message = "Invalid OTP" });
            }

            var user = await _context.Users.FindAsync(userId);
            user.TwoFactorAuthEnabled = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Two-factor authentication enabled successfully" });
        }

        // SEND VERIFICATION OTP
        [Authorize]
        [HttpPost("verification/{verificationType}/send-otp")]
        public async Task<IActionResult> SendVerificationOtp(string verificationType)
        {
            var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            var otp = new Random().Next(100000, 999999).ToString();
            var verificationCode = new VerificationCode
            {
                UserId = userId,
                Email = user.Email,
                Mobile = user.Mobile,
                OTP = otp,
                VerificationType = verificationType
            };

            _context.VerificationCodes.Add(verificationCode);
            await _context.SaveChangesAsync();

            // Simulate sending OTP
            Console.WriteLine($"OTP sent: {otp}");

            return Ok(new { message = "OTP sent successfully" });
        }

        // VERIFY OTP
        [Authorize]
        [HttpPatch("verification/verify-otp/{otp}")]
        public async Task<IActionResult> VerifyOtp(string otp)
        {
            var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var verificationCode = await _context.VerificationCodes
                .FirstOrDefaultAsync(v => v.UserId == userId && v.OTP == otp);

            if (verificationCode == null)
            {
                return BadRequest(new { message = "Invalid OTP" });
            }

            return Ok(new { message = "OTP verified successfully" });
        }

        // SEND RESET PASSWORD OTP
        [HttpPost("reset-password/send-otp")]
        public async Task<IActionResult> SendResetPasswordOtp([FromBody] ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.SendTo);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            var otp = new Random().Next(100000, 999999).ToString();
            var verificationCode = new VerificationCode
            {
                UserId = user.Id,
                Email = user.Email,
                OTP = otp,
                VerificationType = "reset-password"
            };

            _context.VerificationCodes.Add(verificationCode);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Reset Password OTP: {otp}");

            return Ok(new { session = verificationCode.Id });
        }

        // VERIFY RESET PASSWORD OTP
        [HttpPatch("reset-password/verify-otp")]
        public async Task<IActionResult> VerifyResetPasswordOtp([FromBody] ResetPasswordVerifyRequest request)
        {
            var verificationCode = await _context.VerificationCodes
                .FirstOrDefaultAsync(v => v.Id == request.Session && v.OTP == request.Otp);

            if (verificationCode == null)
            {
                return BadRequest(new { message = "Invalid OTP" });
            }

            var user = await _context.Users.FindAsync(verificationCode.UserId);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Password reset successfully" });
        }

    }
}
