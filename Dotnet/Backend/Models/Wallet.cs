using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WealthWise.Models
{
    public class Wallet
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Column(TypeName = "decimal(20,2)")]
        public decimal Balance { get; set; } = 0.00M;

        // Navigation Property
        public virtual ICollection<WalletTransaction> Transactions { get; set; }
    }
}
