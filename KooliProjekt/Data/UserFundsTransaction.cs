namespace KooliProjekt.Data // 13.11 class for tracking funds movement
{
    public enum TransactionType
    {
        Deposit, 
        Withdrawal
    }
    public class UserFundsTransaction // renamed to userFunds Transaction 19.11
    {
        public int FundsTransactionId { get; set; } 
        public int FundID { get; set; } // Foreign key to the UserFunds
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; } // "Deposit" or "Withdrawal"
        // Other relevant fields like TransactionDescription, etc.
        public string? Comment { get; set; } // some comment if necessary for transaction, 
    }
}
