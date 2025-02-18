using System;

namespace WealthWiseAPI.DTOs
{
    public class WithdrawalDTO
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class CreateWithdrawalDTO
    {
        public decimal Amount { get; set; }
    }

    public class ProceedWithdrawalDTO
    {
        public bool Accept { get; set; }
    }
}
