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

    }
}
