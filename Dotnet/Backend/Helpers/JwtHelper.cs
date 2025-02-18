namespace WealthWiseAPI.Helpers
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public class JwtHelper
    {
        private readonly string _key;
        public JwtHelper(string key) => _key = key;

        public string GenerateToken(string email, string role)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: "WealthWiseAPI",
                                             audience: "WealthWiseUsers",
                                             claims: claims,
                                             expires: DateTime.UtcNow.AddMinutes(60),
                                             signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
