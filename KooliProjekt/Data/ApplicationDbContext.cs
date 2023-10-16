using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users {  get; set; }
        public DbSet<Stocks> Stocks { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Portfolio> UsersPortfolios { get; set; }

        //mingil p]hjusel muidu ei teinud stockile ja transactionile primary keyd. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stocks>().HasKey(s => s.StockId);
            modelBuilder.Entity<Transactions>().HasKey(s => s.TransactionId);
        }

    }
}