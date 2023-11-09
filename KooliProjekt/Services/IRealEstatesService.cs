using KooliProjekt.Data;

namespace KooliProjekt.Services// loodud 05.11 t3 raames
{
    public interface IRealEstatesService
    {
        Task <PagedResult<RealEstate>> List(int page, int pageSize); 
        Task<RealEstate> GetById(int id);
        Task Save(RealEstate realEstate);
        Task Delete(int id);
        bool RealEstateExists(int id);
    }
}
