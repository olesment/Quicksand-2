using System.Globalization;
using System.Security.Permissions;

namespace KooliProjekt.Data
{
    public class Transactions
    {
        public int TransactionId {get;set;}
        public DateTime? TransactionTime { get;set;}
        public string? InvestmentType { get;set; }
        public int AssetId { get; set; }
        public string? Action {  get; set; } // Purchase or sell
        public decimal? BalanceBefore { get;set;}
        public int? TransactedAmount {  get;set;}
        public decimal? TransactionUnitCost {  get;set;}
        public decimal? TransactionResult { get;set;} // the sumtotal of transaction
        public decimal? BalanceAfter { get;set;} //balance before -transaction result
        public User? User { get;set;} //
    }
}
