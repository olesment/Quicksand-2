namespace KooliProjekt.Models // 22.11 this is for buying and selling.
{
    public class PurchaseRealEstatesViewModel
    {
        public string? RealEstateName { get; set; }
        public string? RealEstateCountry { get; set; }
        public string? RealEstateCity { get; set; }
        public string? RealEstateAddress { get; set; }
        public decimal? PurchasePrice { get; set; }

    }
}
