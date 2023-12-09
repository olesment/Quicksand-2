using KooliProjekt.Models;
//Gt5 09.12 repo jaoks tehtud interface ja lslt kopisin Real Estates Interfacest k]ik siia sisse. 
namespace KooliProjekt.Data.Repositories
{
    public interface IRealEstatesRepository
    {
        Task<PagedResult<RealEstate>> List(int page, int pageSize);
        Task<RealEstate> GetById(int id);
        Task Save(RealEstate realEstate);
        Task Delete(int id);
        bool RealEstateExists(int id);
        Task<bool> PurchaseRealEstate(PurchaseRealEstatesViewModel model);
        Task<bool> SellRealEstate(int realEstateId, decimal sellingPrice);
    }
}
