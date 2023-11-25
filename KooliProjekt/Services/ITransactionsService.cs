using KooliProjekt.Data;
using KooliProjekt.Models;

namespace KooliProjekt.Services
{
    public interface ITransactionsService
    {
        Task<PagedResult<Transactions>> List(int page, int pageSize);
    }
}
