using WealthWiseAPI.Models;

namespace WealthWiseAPI.Services
{
    public interface IWatchlistService
    {
        Task<Watchlist> GetUserWatchlistAsync(long userId);
        Task<bool> AddCoinToWatchlistAsync(long userId, string coinId);
        Task<bool> RemoveCoinFromWatchlistAsync(long userId, string coinId);
    }
}
