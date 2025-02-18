using Microsoft.EntityFrameworkCore;
using WealthWiseAPI.Data;
using WealthWiseAPI.Models;

namespace WealthWiseAPI.Repositories
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly AppDbContext _context;

        public WatchlistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Watchlist> GetUserWatchlistAsync(long userId)
        {
            return await _context.Watchlists
                .Include(w => w.WatchlistCoins)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<bool> AddCoinToWatchlistAsync(long userId, string coinId)
        {
            var watchlist = await _context.Watchlists.FirstOrDefaultAsync(w => w.UserId == userId);

            if (watchlist == null)
            {
                watchlist = new Watchlist { UserId = userId };
                _context.Watchlists.Add(watchlist);
                await _context.SaveChangesAsync();
            }

            if (!_context.WatchlistCoins.Any(wc => wc.WatchlistId == watchlist.Id && wc.CoinId == coinId))
            {
                _context.WatchlistCoins.Add(new WatchlistCoin { WatchlistId = watchlist.Id, CoinId = coinId });
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveCoinFromWatchlistAsync(long userId, string coinId)
        {
            var watchlist = await _context.Watchlists.FirstOrDefaultAsync(w => w.UserId == userId);

            if (watchlist == null)
                return false;

            var watchlistCoin = await _context.WatchlistCoins
                .FirstOrDefaultAsync(wc => wc.WatchlistId == watchlist.Id && wc.CoinId == coinId);

            if (watchlistCoin != null)
            {
                _context.WatchlistCoins.Remove(watchlistCoin);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
