using WealthWiseAPI.Models;

namespace WealthWiseAPI.Repositories
{
    public interface IWatchlistRepository
    {
        Task<Watchlist> GetUserWatchlistAsync(long userId);
        Task<bool> AddCoinToWatchlistAsync(long userId, string coinId);
        Task<bool> RemoveCoinFromWatchlistAsync(long userId, string coinId);
    }
}
