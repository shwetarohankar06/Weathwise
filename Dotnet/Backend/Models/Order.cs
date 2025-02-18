using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WealthWiseAPI.Models;

namespace WealthWise.Models
{
    public class Order
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public string OrderType { get; set; } // BUY, SELL

        [Column(TypeName = "decimal(20,2)")]
        public decimal Price { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        public string Status { get; set; } // PENDING, COMPLETED, CANCELED

        public long OrderItemId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("OrderItemId")]
        public OrderItem OrderItem { get; set; }
    }
}
