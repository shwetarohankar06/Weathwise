using System.Collections.Generic;
using System.Threading.Tasks;
using WealthWiseAPI.DTOs;
using WealthWiseAPI.Models;
using WealthWiseAPI.Repositories;

namespace WealthWiseAPI.Services
{
    public class WithdrawalService
    {
        private readonly IWithdrawalRepository _withdrawalRepository;

        public WithdrawalService(IWithdrawalRepository withdrawalRepository)
        {
            _withdrawalRepository = withdrawalRepository;
        }

        public async Task<IEnumerable<WithdrawalDTO>> GetAllWithdrawalsAsync()
        {
            var withdrawals = await _withdrawalRepository.GetAllWithdrawalsAsync();
            return withdrawals.Select(w => new WithdrawalDTO
            {
                Id = w.Id,
                Amount = w.Amount,
                Status = w.Status,
                Date = w.Date,
                UserId = w.UserId,
                FullName = w.User?.FullName,
                Email = w.User?.Email
            });
        }

        public async Task<Withdrawal> CreateWithdrawalAsync(long userId, CreateWithdrawalDTO dto)
        {
            var withdrawal = new Withdrawal
            {
                UserId = userId,
                Amount = dto.Amount,
                Status = "PENDING"
            };

            return await _withdrawalRepository.CreateWithdrawalAsync(withdrawal);
        }

        public async Task<Withdrawal> ProceedWithdrawalAsync(long withdrawalId, bool accept)
        {
            var withdrawal = await _withdrawalRepository.GetWithdrawalByIdAsync(withdrawalId);
            if (withdrawal == null) return null;

            withdrawal.Status = accept ? "APPROVED" : "REJECTED";
            return await _withdrawalRepository.UpdateWithdrawalAsync(withdrawal);
        }
    }
}
