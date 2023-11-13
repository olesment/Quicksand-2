namespace KooliProjekt.Data
{
    public class UserFunds
    {
        //User UserID
        public int FundID { get; set; }
        public string? FundName { get; set; } = "Euros";// 13.11 praegu eurod
        public decimal? Balance { get; set; } // vabad vahendid uuteks investeeringuteks v\i v2ljav6tmiseks. 
        public decimal? DepositedFunds { get; set; } // platvormile kantud vahendid
        public decimal? LockedFunds {  get; set; } //vahendid mison investeeringute all kinni
       // public decimal? TotalFunds { get; set; } // vahendid kokku


    }
}
