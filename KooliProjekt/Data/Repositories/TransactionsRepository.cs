using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TransactionsViewModel>> List(int page, int pageSize)
        {
            var transactionsQueryForTransactionsViewModel = _context.Transactions.Select(t => new TransactionsViewModel

            {
                TransactionTime = t.TransactionTime,
                TransactionID = t.TransactionId,
                AssetId = t.AssetId,

                AssetName = _context.RealEstates.Where(re => re.RealEstateId == t.AssetId)
                                                .Select(re => re.RealEstateName)
                                                .FirstOrDefault(),

                Action = t.Action,
                BalanceBefore = t.BalanceBefore,
                TransactedUnitAmount = t.TransactedAmount,
                TransactionUnitCost = t.TransactionUnitCost,
                TransactionSum = t.TransactionResult,
                BalanceAfter = t.BalanceAfter,
                AssetType = t.InvestmentType

            });
            var result = await transactionsQueryForTransactionsViewModel.GetPagedAsync(page, pageSize: 3);
            return result;
        }

        public async Task<Transactions> GetById(int id)
        {
            var result = await _context.Transactions.FirstOrDefaultAsync(m => m.TransactionId == id);
            return result;
        }

        public async Task Save(Transactions transaction)
        {
            if (transaction.TransactionId == null)
            {
                await _context.AddAsync(transaction);
            }
            else
            {
                _context.Update(transaction);
            }
            //await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
            await _context.SaveChangesAsync();
        }
    }
}
