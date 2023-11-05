using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class RealEstate
    {
        public int RealEstateId { get; set; }
        public string? RealEstateName { get; set; }
        public string? RealEstateCountry { get; set; }
        public string? RealEstateCity { get; set; }
        public string? RealEstateAddress { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? CurrentValue { get; set; }
        [DataType(DataType.Date)]
        public DateTime? LastCurrentValueChangeTime {  get; set; }
        public string? InvestmentType { get; } = "RealEstate";

       // public User User { get; set; } kuidagi peab useriga ju siduma
        public bool CurrentlyOwned { get; set; }
    }
}
