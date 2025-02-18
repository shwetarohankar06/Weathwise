using System.Collections.Generic;
using System.Threading.Tasks;
using WealthWiseAPI.Models;

namespace WealthWiseAPI.Repositories
{
    public interface IWithdrawalRepository
    {
        Task<IEnumerable<Withdrawal>> GetAllWithdrawalsAsync();
        Task<IEnumerable<Withdrawal>> GetUserWithdrawalsAsync(long userId);
        Task<Withdrawal> GetWithdrawalByIdAsync(long id);
        Task<Withdrawal> CreateWithdrawalAsync(Withdrawal withdrawal);
        Task<Withdrawal> UpdateWithdrawalAsync(Withdrawal withdrawal);
    }
}
