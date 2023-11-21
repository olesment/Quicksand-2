using KooliProjekt.Data; //loodud 19.11 T4 raames. 
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace KooliProjekt.Services
{
    public class UserFundsTransactionsService : IUserFundsTransactionsService
    {
        private readonly ApplicationDbContext _context;
        
        public UserFundsTransactionsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<UserFundsTransaction>>List(int page, int pageSize)
        {
            var result = await _context.UserFundsTransactions.GetPagedAsync(page, /*pageSize:*/ 3);
            return result;
        }

        public async Task<UserFundsTransaction>GetById(int id)
        {
            var result = await _context.UserFundsTransactions
                .FirstOrDefaultAsync(m => m.FundsTransactionId == id);
            return result; 
        }
        //DEPOSIT
        public async Task<bool> Deposit(int fundID, decimal amount, string comment)
        {
            if (amount <= 0)
            {
                return false;
            }

            var userFund = await _context.UserFunds.FindAsync(fundID);
            if (userFund == null) return false;

            userFund.Balance += amount;
            userFund.DepositedFunds += amount;

            //ylekande kirje tegemine 19.11
            var fundsTransaction = new UserFundsTransaction
            {
                //19.11 FundsTransactionId = fundsTransactionId, seda pole vist vaja panna sest uue kirje tegemeisega peaks see autoincrementima sest see on primary key
                FundID = fundID,
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Deposit,
                Comment = comment
                //19.11 Comment ma ei tea kas siia peaks ka selle kommentaari lisamise v]imaluse kuidagi tegema. 
            };
            _context.UserFundsTransactions.Add(fundsTransaction);
            await _context.SaveChangesAsync();
            return true;

        }
        //just adding random comment so i can make a commit. with long description in a new branch. .
        public async Task<bool> Withdraw(int fundID, decimal amount, string comment)
        {
            if (amount <= 0)
            {
                return false;
            }
            var userFund = await _context.UserFunds.FindAsync(fundID);
            if (userFund == null || userFund.Balance < amount) return false;

            userFund.Balance -= amount;
            userFund.WithdrawnFunds += amount;

            var fundsTransaction = new UserFundsTransaction
            {
                FundID = fundID,
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Withdrawal,
                Comment = comment
            };
            _context.UserFundsTransactions.Add(fundsTransaction);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}

