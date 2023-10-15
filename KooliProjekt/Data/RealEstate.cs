namespace KooliProjekt.Data
{
    public class RealEstate
    {
        public int RealEstateId { get; set; }
        public string? RealEstateName { get; set; }
        public string? RealEstateCountry { get; set; }
        public string? RealEstateCity { get; set; }
        public string? RealEstateAddress { get; set; }
        public DateOnly? PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? CurrentValue { get; set; }
        public DateOnly? LastCurrentValueChangeTime {  get; set; }
        public string? InvestmentType { get; } = "RealEstate";
        public User User { get; set; }
        public bool CurrentlyOwned { get; set; }
    }
}
