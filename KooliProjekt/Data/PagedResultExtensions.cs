using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data
{
    public static class PagedResultExtensions
    {
        public static async Task<PagedResult<Placeholder_Table>> GetPagedAsync<Placeholder_Table>(this IQueryable<Placeholder_Table> query, int page, int pageSize)
        {
            page = Math.Max(page, 1);
            var result = new PagedResult<Placeholder_Table>
            {
                CurrentPage = page,
                PageSize = pageSize, 
                RowCount = await query.CountAsync()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int) Math.Ceiling(pageCount); // ceiling ymardab alati yles. 31.10

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}
