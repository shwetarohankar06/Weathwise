using WealthWise.DTOs;

namespace WealthWise.Services
{
    public interface IWalletService
    {
        Task<WalletDto> GetWalletBalance(long userId);
        Task<List<WalletTransactionDto>> GetWalletTransactions(long userId);
        Task<bool> DepositFunds(long userId, decimal amount, string paymentId);
        Task<bool> WithdrawFunds(long userId, decimal amount);
    }
}
