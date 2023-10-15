using KooliProjekt.Data;
using System.Xml.Schema;

namespace KooliProjekt.Data
{
    public class Portfolio
    {
        //should be things that are common in different assets
        public int PortfolioId { get; set; }
        public User User { get; set; } //This should link assets specific to user into the view
        public DateTime SnapshotDate { get; set; }
        public string? InvestmentType { get; set; } // Should be those types set in respectful classes
        public decimal? TotalInvested { get; set; }
        public decimal? CurrentAggregateValue { get; set; }
        public decimal? InvestedInStocksValue { get; set; }
        public decimal? CurrentAggregateStockValue { get; set; }
        public decimal? InvestedInRealestate { get; set; }
        public decimal? CurrentAggregateRealEstateValue { get; set; }
        public decimal? FreeFunds { get; set; }
        public List<Transactions> RecentTransactionsList { get; set; }

    }

    public class UsersStocksPortfolio
    {
        //should only be things with investment type stocks
        public User User { get; set; } // should be same user as above
        public decimal? CurrentAggregateStockValue { get; set; }
        public decimal? InitiallyInvested {  get; set; } // how much was invested originally
        public decimal? CurrentParticularStockValue {  get; set; }
        public int? StockAmount { get; set; } // amount of shares
        public List<Stocks>? UserStockList { get; set; }

    }
    public class UsersRealEstatePortfolio
    {
        
        public User User { get; set; }
        public decimal? InvestedInRealestate { get; set; }
        public decimal? InitialInvestmentPerRealty {  get; set; }
        public decimal? CurrentParticularRealtyValue { get; set; }
        public decimal? CurrentAggregateRealEstateValue { get; set; }
        public List<RealEstate> UserRealEstateList { get; set; }

    }

    //public class RecentTransactions
    //{
    //    public User User { get; set; }
        
    //}
    

}
