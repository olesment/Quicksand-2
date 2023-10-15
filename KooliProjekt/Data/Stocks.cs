using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Stocks
    {
        public int StockId {  get; set; }
        public string? StockName { get; set;}
        public string? StockTicker {  get; set; }

        [DataType(DataType.Date)]
        public DateTime? StockInvestmentDate{ get; set; }
        public decimal? StockAmount { get; set; }
        public int? StockBuyingPrice { get; set; }
        public decimal? InvestedInParticluarStock { get; set; }
        [DataType(DataType.Date)]
        public DateTime? LastClosingDate { get; set; }
        public decimal? LastClosingPrice { get; set; }
        public string? InvestmentType { get; } = "Stocks";
        public User? User { get; set; }
        public bool? CurrentlyOwned { get; set; }
    }
}
