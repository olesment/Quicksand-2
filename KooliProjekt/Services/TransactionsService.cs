using KooliProjekt.Data; //loodud 05.11 T3 raames. 
using KooliProjekt.Models;
using KooliProjekt.Data.Repositories;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Xml;

namespace KooliProjekt.Services //25.11
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _repository;

        public TransactionsService(ITransactionsRepository repository)
        {
            _repository = repository;
        }

        /*public async Task<PagedResult<Transactions>> List(int page, int pageSize) // pole kindel mis sisemisesse <> l'heb 05.11
        //{
        //    var result = await _context.Transactions.GetPagedAsync(page, pageSize: 3);

        //    return result;
        /*}*/ //PROOvin siin teha nii et annaks tagasi mulle sobiliku view modeli. 

        public async Task<PagedResult<TransactionsViewModel>> List(int page, int pageSize)
        {
            var result = await _repository.List(page , pageSize: 3);
            return result;
            //^Pelae Repositorysse viimist 10.12.
            //var transactionsQueryForTransactionsViewModel = _context.Transactions.Select(t => new TransactionsViewModel
            
            //{
            //    TransactionTime = t.TransactionTime,
            //    TransactionID = t.TransactionId,
            //    AssetId = t.AssetId,

            //    AssetName = _context.RealEstates.Where(re => re.RealEstateId == t.AssetId)
            //                                    .Select(re => re.RealEstateName)
            //                                    .FirstOrDefault(),

            //    Action = t.Action,
            //    BalanceBefore = t.BalanceBefore,
            //    TransactedUnitAmount = t.TransactedAmount,
            //    TransactionUnitCost = t.TransactionUnitCost,
            //    TransactionSum = t.TransactionResult,
            //    BalanceAfter = t.BalanceAfter,
            //    AssetType = t.InvestmentType

            //});
            //var result = await transactionsQueryForTransactionsViewModel.GetPagedAsync(page, pageSize: 3);
            //return result;
        }

        //28.11 lisan getByID
        public async Task<Transactions>GetById(int id)
        {
            var result = await _repository.GetById(id);// context.Transactions.FirstOrDefaultAsync(m=>m.TransactionId == id);
            return result;
        }

        //28.11 Lisatud Save. Save eraldi nagu kontrolleris ei ole, see oleks pigem nagu helper vms 
        public async Task Save(Transactions transaction)
        {
            //10.12 yle viidud repositorysse. 
            await _repository.Save(transaction);


            //if (transaction.TransactionId == null)
            //{
            //    _context.Add(transaction);
            //}
            //else
            //{
            //    _context.Update(transaction);
            //}
            //await _context.SaveChangesAsync();
        }

        //28.11 Delete - arvan et see ei peaks olema transactionite funktsionaalsuses

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
            //var transaction = await _context.Transactions.FindAsync(id);
            //if(transaction !=null)
            //{
            //    _context.Transactions.Remove(transaction);
            //}
            //await _context.SaveChangesAsync();
        }

    }
}
