using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class SellRealEstatesViewModel 
        //25-11, withdraw jaoks peaks olema nii, et ostetud Re jaoks peaks vast 
        //Re taga, ridadel olema sell nupp ja seal siis vajutad ja täidad hinda. 
    {
        public RealEstate RealEstate { get; set; } 
        // 25.11 so im selling an instance of real estate?
        public decimal? SellingPrice { get; set; }
    }
}
