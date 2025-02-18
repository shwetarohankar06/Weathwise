using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WealthWiseAPI.Models
{
    public class Withdrawal
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Status { get; set; } = "PENDING"; // Default Status

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
