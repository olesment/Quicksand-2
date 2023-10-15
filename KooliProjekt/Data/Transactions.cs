using System.Globalization;

namespace KooliProjekt.Data
{
    public class Transactions
    {
        public int TransactionId {get;set;}
        public DateTime? TransactionTime { get;set;}
        public string? InvestmentType { get;set; }
        public int AssetId { get; set; }
        public string? Action {  get; set; } // buy or sell
        public int? TransactedAmount {  get;set;}
        public decimal? TransactionUnitCost {  get;set;}
        public decimal? TransactionResult { get;set;} // the sumtotal of transaction
        public User User { get;set;}

    }
}
