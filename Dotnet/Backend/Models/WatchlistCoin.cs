using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WealthWiseAPI.Models
{
    public class WatchlistCoin
    {
        [Required]
        public long WatchlistId { get; set; }

        [Required]
        public string CoinId { get; set; }  // Refers to Coins table

        [ForeignKey("WatchlistId")]
        public Watchlist Watchlist { get; set; }
    }
}
