using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WealthWiseAPI.Data;
using WealthWiseAPI.Models;

namespace WealthWiseAPI.Repositories
{
    public class WithdrawalRepository : IWithdrawalRepository
    {
        private readonly AppDbContext _context;

        public WithdrawalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Withdrawal>> GetAllWithdrawalsAsync()
        {
            return await _context.Withdrawals.Include(w => w.User).ToListAsync();
        }

        public async Task<IEnumerable<Withdrawal>> GetUserWithdrawalsAsync(long userId)
        {
            return await _context.Withdrawals
                .Include(w => w.User)
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task<Withdrawal> GetWithdrawalByIdAsync(long id)
        {
            return await _context.Withdrawals.Include(w => w.User).FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Withdrawal> CreateWithdrawalAsync(Withdrawal withdrawal)
        {
            _context.Withdrawals.Add(withdrawal);
            await _context.SaveChangesAsync();
            return withdrawal;
        }

        public async Task<Withdrawal> UpdateWithdrawalAsync(Withdrawal withdrawal)
        {
            _context.Withdrawals.Update(withdrawal);
            await _context.SaveChangesAsync();
            return withdrawal;
        }
    }
}
