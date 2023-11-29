namespace KooliProjekt.Models
{
    public class TransactionsViewModel // siin peaksid istuma k]ik parameetrid,
                                       // mis kokkuv\ttes ehitavad kokku viewModeli v]ttes andmeid tiestest
                                       // tabelistest ja kombineerides neid ylevaatl=ikuks tabeliks
    {
        public DateTime? TransactionTime { get; set; }
        public int? TransactionID { get; set; }
        public int? AssetId { get; set; }
        public string? AssetName { get; set; }
        public string? Action { get; set; } // buy or sell
        public decimal? BalanceBefore {  get; set; } // how much was in the wallet before
        public int? TransactedUnitAmount { get; set; } // how many assets were trasferred
        public decimal? TransactionUnitCost {  get; set; } //how much one piece cost
        public decimal? TransactionSum {  get; set; }
        public decimal? BalanceAfter { get; set; }
        public string? AssetType { get; set; } // realestate or crypto or whatever 


    }
}
