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
        public double? BalanceBefore {  get; set; } // how much was in the wallet before
        public int? TransactedUnitAmount { get; set; } // how many assets were trasferred
        public int? TransactionUnitCost {  get; set; } //how much one piece cost
        public double? TransactionSum {  get; set; }
        public double? BalanceAfter { get; set; }
        public string? AssetType { get; set; } // realestate or crypto or whatever 


    }
}
