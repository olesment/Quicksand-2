using KooliProjekt.Data;
using KooliProjekt.Models;

namespace KooliProjekt.Services
{
    public interface ITransactionsService
    {
        Task<PagedResult<Transactions>> List(int page, int pageSize);
        //28.11
        Task<Transactions> GetById(int id);
        Task Save(Transactions transaction);
    }
}
