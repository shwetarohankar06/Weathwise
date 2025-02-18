using Microsoft.EntityFrameworkCore;
using WealthWise.Models;
using WealthWiseAPI.Models;

namespace WealthWiseAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<WatchlistCoin> WatchlistCoins { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<CoinData> CoinData { get; set; }
        public DbSet<WatchlistUser> watchlistUsers { get; set; }
        public DbSet<UserBuy> userBuys { get; set; }

        //public DbSet<Asset> Assets { get; set; }
        //        public DbSet<Withdrawal> Withdrawals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().ToTable("Wallets");
            modelBuilder.Entity<WalletTransaction>().ToTable("WalletTransactions");
            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.Transactions)
                .WithOne(t => t.Wallet)
                .HasForeignKey(t => t.WalletId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WatchlistCoin>()
                .HasKey(wc => new { wc.WatchlistId, wc.CoinId });

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();


            modelBuilder.Entity<Withdrawal>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
