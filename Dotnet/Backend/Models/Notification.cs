using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WealthWiseAPI.Models
{
    public class Notification
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long FromUserId { get; set; }

        [Required]
        public long ToUserId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(255)]
        public string Message { get; set; }

        [ForeignKey("FromUserId")]
        public virtual User FromUser { get; set; }

        [ForeignKey("ToUserId")]
        public virtual User ToUser { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
