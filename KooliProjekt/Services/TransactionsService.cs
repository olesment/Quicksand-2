using KooliProjekt.Data; //loodud 05.11 T3 raames. 
using KooliProjekt.Models;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Xml;

namespace KooliProjekt.Services //25.11
{
    public class TransactionsService: ITransactionsService
    {
        private readonly ApplicationDbContext _context;
        
            public TransactionsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Transactions>> List(int page, int pageSize) // pole kindel mis sisemisesse <> l'heb 05.11
        {
            var result = await _context.Transactions.GetPagedAsync(page, pageSize: 3);

            return result;
        }
        //28.11 lisan getByID
        public async Task<Transactions>GetById(int id)
        {
            var result = await _context.Transactions.FirstOrDefaultAsync(m=>m.TransactionId == id);
            return result;
        }

        //28.11 Lisatud Save. Save eraldi nagu kontrolleris ei ole, see oleks pigem nagu helper vms 
        public async Task Save(Transactions transaction)
        {
            if (transaction.TransactionId == null)
            {
                _context.Add(transaction);
            }
            else
            {
                _context.Update(transaction);
            }
            await _context.SaveChangesAsync();
        }

        //28.11 Delete - arvan et see ei peaks olema transactionite funktsionaalsuses

        public async Task Delete(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if(transaction !=null)
            {
                _context.Transactions.Remove(transaction);
            }
            await _context.SaveChangesAsync();
        }

    }
}
