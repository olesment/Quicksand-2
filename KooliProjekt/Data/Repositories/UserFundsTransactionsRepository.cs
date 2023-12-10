namespace KooliProjekt.Data.Repositories
{
    public class UserFundsTransactionsRepository : IUserFundsTransactionsRepository
    {
        private readonly ApplicationDbContext _context;
        public UserFundsTransactionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
