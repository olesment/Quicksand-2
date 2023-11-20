using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IUserFundsTransactionsService
    {
        Task<bool> Deposit(int fundID, decimal amount, string comment);
        Task<bool> Withdraw(int fundID, decimal amount, string comment);
        Task<PagedResult<UserFundsTransaction>> List(int page, int pageSize);
        //sellel on kuskil see pager htmlis puudu 
        Task<UserFundsTransaction> GetById(int id);
    }
}
