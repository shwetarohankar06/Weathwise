using System.ComponentModel.DataAnnotations;

public class VerificationCode
{
    public long Id { get; set; }

    [Required, MaxLength(50)]
    public string OTP { get; set; }

    public long UserId { get; set; }

    [EmailAddress, MaxLength(255)]
    public string Email { get; set; }

    [MaxLength(20)]
    public string Mobile { get; set; }

    [MaxLength(50)]
    public string VerificationType { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
