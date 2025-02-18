using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WealthWiseAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(255)]
        public string FullName { get; set; }

        [Required, MaxLength(255), EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; }

        [Required]
        public string Password { get; set; }

        public string Status { get; set; }
        public bool IsVerified { get; set; }
        public bool TwoFactorAuthEnabled { get; set; }
        public string TwoFactorAuthSendTo { get; set; }
        public string Picture { get; set; }
        public string Role { get; set; } = "User";
    }
}
