using WealthWiseAPI.Models;
using WealthWiseAPI.Repositories;

namespace WealthWiseAPI.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository _repository;

        public WatchlistService(IWatchlistRepository repository)
        {
            _repository = repository;
        }

        public async Task<Watchlist> GetUserWatchlistAsync(long userId)
        {
            return await _repository.GetUserWatchlistAsync(userId);
        }

        public async Task<bool> AddCoinToWatchlistAsync(long userId, string coinId)
        {
            return await _repository.AddCoinToWatchlistAsync(userId, coinId);
        }

        public async Task<bool> RemoveCoinFromWatchlistAsync(long userId, string coinId)
        {
            return await _repository.RemoveCoinFromWatchlistAsync(userId, coinId);
        }
    }
}
