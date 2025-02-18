namespace WealthWise.DTOs
{
    public class WalletTransactionDto
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string TransferId { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
    }
}
