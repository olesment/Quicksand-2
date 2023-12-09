using KooliProjekt.Data; //loodud 05.11 T3 raames. 
using KooliProjekt.Data.Repositories;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Xml;

namespace KooliProjekt.Services
{
    public class RealEstatesService : IRealEstatesService
    {
        private readonly IRealEstatesRepository _repository;/*ApplicationDbContext*/ /*_context*/ //Muudetud T5 k'igus 09.12/ 
        public RealEstatesService(IRealEstatesRepository repository)
        {
            _repository = repository;
        }
        // Gt5 09.12 yle viidud repositorysse //
        public async Task<PagedResult<RealEstate>> List(int page, int pageSize) // pole kindel mis sisemisesse <> l'heb 05.11
        {
            var result = await _repository.List(page, pageSize: 3);

            return result;
        }
        public async Task<RealEstate> GetById(int id)
        {
            var result = await _repository.GetById(id);

            return result;
        }
        // Gt5 09.12 yle viidud repositorysse //

        // Gt5 09.12 yle viidud repositorysse //
        public async Task Save(RealEstate realEstate)
        {
            await _repository.Save(realEstate);
            //if (realEstate.RealEstateId == null)
            //{
            //    _context.Add(realEstate);
            //}
            //else
            //{
            //    _context.Update(realEstate);
            //}
            //await _context.SaveChangesAsync();
        }
        // Gt5 09.12 yle viidud repositorysse //
        // Gt5 09.12 yle viidud repositorysse //
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
            //var realEstate = await _context.RealEstates.FindAsync(id);
            //if (realEstate != null)
            //{
            //    _context.RealEstates.Remove(realEstate);
            //}
            //await _context.SaveChangesAsync();
        }
        // Gt5 09.12 yle viidud repositorysse //
        public bool RealEstateExists(int id) // 09.12 IDK KAS SEDA ASJA ON YLDSE VAJA.
        {
            return _repository.RealEstateExists(id);

        }
        // Gt5 09.12 yle viidud repositorysse //

        public async Task<bool> PurchaseRealEstate(PurchaseRealEstatesViewModel model)
        {
            return await _repository.PurchaseRealEstate(model);

            // 09.12 tegin t5 moodi. ~tt 1:07. 
            //var userFunds = await _repository.PurchaseRealEstate
            //if (userFunds.Balance < model.PurchasePrice)
            //{
            //    return false;
            //}

            ////29.11 lisatud peale seda kui transactions tabelit muutsin
            //var balanceBefore = userFunds.Balance.Value;

            //var newRealEstate = new RealEstate
            //{
            //    RealEstateName = model.RealEstateName,
            //    RealEstateCountry = model.RealEstateCountry,
            //    RealEstateCity = model.RealEstateCity,
            //    RealEstateAddress = model.RealEstateAddress,
            //    PurchaseDate = DateTime.UtcNow,
            //    PurchasePrice = model.PurchasePrice,
            //    CurrentValue = model.PurchasePrice,
            //    LastCurrentValueChangeTime = DateTime.UtcNow,
            //    //InvestmentType = "RealEstate", // automaateselt pandud
            //    CurrentlyOwned = true

            //};
            //_context.RealEstates.Add(newRealEstate);

            //userFunds.Balance -= model.PurchasePrice.Value;// Kodra tehtud viga ili cshtmlis// siin istub mingi viga, mis genereerib nein NULL e
            //userFunds.LockedFunds += model.PurchasePrice.Value;

            //await _context.SaveChangesAsync(); // see salvestab tehingu mille k'igus genereeritakse uus assetID, mida saab kasutada tehingu [leskirjutamiseks Transactions tabelisse.

            //var balanceAfter = userFunds.Balance.Value;
            //var lockedFunds = userFunds.LockedFunds.Value;
            //var transactionResult = 0;
            //var transactedAmount = 1;
            //    balanceAfter -= model.PurchasePrice.Value;
            //    lockedFunds += model.PurchasePrice.Value;
            //    transactionResult = (int)model.PurchasePrice.Value * transactedAmount; //int vs decimal

            //var transactionRecord = new Transactions
            //{
            //    //TransactionId should autoincrement 
            //    TransactionTime = newRealEstate.PurchaseDate,
            //    InvestmentType = "RealEstate",
            //    AssetId = newRealEstate.RealEstateId,
            //    Action = "Purchase",
            //    BalanceBefore = balanceBefore,
            //    TransactedAmount = 1,
            //    TransactionUnitCost = newRealEstate.PurchasePrice.Value,
            //    TransactionResult = transactionResult,
            //    LockedFunds = lockedFunds,
            //    BalanceAfter = balanceAfter, 
            //    LossOrProfit = 0

            //};
            //_context.Transactions.Add(transactionRecord);
            //await _context.SaveChangesAsync();
            //return true;

        }
        //25.11
        public async Task<bool> SellRealEstate(int realEstateId, decimal sellingPrice)
        {
            
            return await _repository.SellRealEstate(realEstateId, sellingPrice);
            //because its bool it gave error if i didnt have return. 
        }
        



        // 09.12 ORIGINAALVARIANT MIS ANDIS IKKA ERROTIT kui proovisin vahetada _repository vastu> 

        //public async Task<bool> SellRealEstate(int realEstateId, decimal sellingPrice)
        //{
        //    await _repository.SellRealEstate(realEstateId, sellingPrice);
        //    var realEstate = await _context.RealEstates.FindAsync(realEstateId);
        //    if (realEstate == null || !realEstate.CurrentlyOwned)
        //    {
        //        return false;
        //    }
        //    var userFunds = await _context.UserFunds.FirstOrDefaultAsync();

        //    if (userFunds == null)
        //    {
        //        return false;
        //    }

        //    userFunds.Balance += sellingPrice;
        //    userFunds.LockedFunds -= sellingPrice;
        //    realEstate.CurrentlyOwned = false;

        //    // realEstate.CurrentValue = sellingPrice;
        //    var transactionRecord = new Transactions
        //    {
        //        TransactionTime = DateTime.UtcNow,
        //        InvestmentType = "RealEstate",
        //        AssetId = realEstateId,
        //        Action = "Sell",
        //        TransactedAmount = 1,
        //        TransactionUnitCost = sellingPrice,
        //        TransactionResult = sellingPrice,

        //    };

        //    _context.Transactions.Add(transactionRecord);
        //    await _context.SaveChangesAsync();
        //    return true;

        //// 09.12 ORIGINAALVARIANT MIS ANDIS IKKA ERROTIT> 
    }
}