using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WealthWiseAPI.Models
{
    public class Watchlist
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }  // Foreign Key

        public List<WatchlistCoin> WatchlistCoins { get; set; } = new List<WatchlistCoin>();
    }
}
