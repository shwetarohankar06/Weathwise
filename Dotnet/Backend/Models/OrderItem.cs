using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WealthWiseAPI.Models;

namespace WealthWise.Models
{
    public class OrderItem
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public int CoinId { get; set; }  // Ensure this is an 'int' to match Coin.Id

        // public string CoinId { get; set; } // Foreign key from Coins table



        [Column(TypeName = "decimal(20,2)")]
        public decimal BuyPrice { get; set; }

        [Column(TypeName = "decimal(20,2)")]
        public decimal SellPrice { get; set; }

        public long OrderId { get; set; }   

        [ForeignKey("CoinId")]
        public Coin Coin { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
