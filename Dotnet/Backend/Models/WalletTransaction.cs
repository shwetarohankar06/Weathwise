using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WealthWise.Models
{
    public class WalletTransaction
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long WalletId { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; } // Deposit, Withdrawal, Transfer

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [StringLength(255)]
        public string TransferId { get; set; } // For external transactions

        [StringLength(255)]
        public string Purpose { get; set; }

        [Required]
        [Column(TypeName = "decimal(20,2)")]
        public decimal Amount { get; set; }

        // Navigation Property
        [ForeignKey("WalletId")]
        public virtual Wallet Wallet { get; set; }
    }
}
