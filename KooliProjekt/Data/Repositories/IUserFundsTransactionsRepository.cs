namespace KooliProjekt.Data.Repositories
{
    public interface IUserFundsTransactionsRepository
    {
        Task<PagedResult<UserFundsTransaction>> List(int page, int pageSize);
        Task<UserFundsTransaction> GetById(int id);
        Task<bool> Deposit(int fundID, decimal amount, string comment);
        Task<bool> Withdraw(int fundID, decimal amount, string comment);
    }
}
