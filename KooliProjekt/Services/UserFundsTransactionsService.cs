using KooliProjekt.Data; //loodud 19.11 T4 raames. 
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class UserFundsTransactionsService : IUserFundsTransactionsService
    {
        private readonly ApplicationDbContext _context;

        public UserFundsTransactionsService(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> Deposit(int fundID, decimal amount)
        //{
        //    var userFund = await _context.UserFunds.FindAsync(fundID);
        //    if (userFund == null) return false;

        //    userFund.Balance += amount;
        //    userFund.DepositedFunds += amount;

        //    //ylekande kirje tegemine 19.11

            
        //}
    }
}

