using KooliProjekt.Models;

namespace KooliProjekt.Data.Repositories
{
    public interface ITransactionsRepository
    {
        Task<PagedResult<TransactionsViewModel>> List(int page, int pageSize);
        Task<Transactions> GetById(int id);
        Task Save(Transactions transaction);
        Task Delete(int id);

    }
}
