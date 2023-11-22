namespace KooliProjekt.Models //22.11 loodud view model selleks et vaadata mis on rahakotis erinevates vaadetes. Kuigi mul on Userfundis juba balance, siin tahan ainult viewd ja see peaks fetchima balanci User fundist tagataustal. Vist. 
{
    public class UserFundsStatusViewModel
    {
        //22.11 userID? Kas see peaks kuidagi seotud olem konkreetse kasutajaga> 
        //22.11 FundID kuna mul on praegu see ripats, et yhel kasutajal v6iks jutskui olla mitu rahakotti, nt eurode ja taalade jaoks eraldi
        //siis peaks vast see Fund ID ka siin olema. Ma ei tea kuidas seda preagu edasi arvestada ja mis muudatused see l]puks kaasa toob. 
        public decimal? UserFundsBalance { get; set; }
    }
}
