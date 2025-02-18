namespace WealthWiseAPI.DTOs
{
    public class NotificationDto
    {
        public long Id { get; set; }
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
