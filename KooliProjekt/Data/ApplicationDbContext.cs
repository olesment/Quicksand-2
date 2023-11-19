using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

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

        //19.11 tabelite lisamine
        public DbSet<UserFunds> UserFunds { get; set; }
        public DbSet<UserFundsTransaction> UserFundsTransactions { get; set; }

        //mingil p]hjusel muidu ei teinud stockile ja transactionile primary keyd. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stocks>().HasKey(s => s.StockId);
            modelBuilder.Entity<Transactions>().HasKey(s => s.TransactionId);
            modelBuilder.Entity<UserFundsTransaction>().HasKey(uft => uft.FundsTransactionId);
            modelBuilder.Entity<UserFunds>().HasKey(uf => uf.FundID);
        }

        //mingil p]hjusel muidu ei teinud stockile ja transactionile primary keyd. 
        //public DbSet<KooliProjekt.Data.UserFundsTransaction>? FundsTransaction { get; set; }

    }
}