
using WealthWise.DTOs;
using WealthWise.Models;
using Microsoft.EntityFrameworkCore;
using WealthWiseAPI.Data;

namespace WealthWise.Services
{
    public class WalletService : IWalletService
    {
        private readonly AppDbContext _context;

        public WalletService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WalletDto> GetWalletBalance(long userId)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            return wallet == null ? null : new WalletDto { Id = wallet.Id, Balance = wallet.Balance };
        }

        public async Task<List<WalletTransactionDto>> GetWalletTransactions(long userId)
        {
            var transactions = await _context.WalletTransactions
                .Where(t => t.Wallet.UserId == userId)
                .Select(t => new WalletTransactionDto
                {
                    Id = t.Id,
                    Type = t.Type,
                    Date = t.Date,
                    TransferId = t.TransferId,
                    Purpose = t.Purpose,
                    Amount = t.Amount
                })
                .ToListAsync();

            return transactions;
        }

        public async Task<bool> DepositFunds(long userId, decimal amount, string paymentId)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null) return false;

            wallet.Balance += amount;

            _context.WalletTransactions.Add(new WalletTransaction
            {
                WalletId = wallet.Id,
                Type = "Deposit",
                TransferId = paymentId,
                Purpose = "Funds Added",
                Amount = amount
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> WithdrawFunds(long userId, decimal amount)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null || wallet.Balance < amount) return false;

            wallet.Balance -= amount;

            _context.WalletTransactions.Add(new WalletTransaction
            {
                WalletId = wallet.Id,
                Type = "Withdrawal",
                Purpose = "Funds Withdrawn",
                Amount = amount
            });

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
