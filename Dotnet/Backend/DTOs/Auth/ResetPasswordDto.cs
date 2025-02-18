public class ResetPasswordRequest
{
    public string SendTo { get; set; }
    public string VerificationType { get; set; }
}

public class ResetPasswordVerifyRequest
{
    public long Session { get; set; }
    public string Otp { get; set; }
    public string Password { get; set; }
}
