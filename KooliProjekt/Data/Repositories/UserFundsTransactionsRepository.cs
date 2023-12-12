namespace KooliProjekt.Data.Repositories
{
    public class UserFundsTransactionsRepository : IUserFundsTransactionsRepository
    {
        private readonly ApplicationDbContext _context;
        public UserFundsTransactionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<UserFundsTransaction>>List(int page, int pageSize)
        {
            var result = await _context.UserFundsTransactions.GetPagedAsync(page, pageSize: 3);
            return result;
        }
    }
}
