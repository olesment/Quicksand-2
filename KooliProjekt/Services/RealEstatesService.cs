using KooliProjekt.Data; //loodud 05.11 T3 raames. 
using KooliProjekt.Models;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Xml;

namespace KooliProjekt.Services
{
    public class RealEstatesService : IRealEstatesService
    {
        private readonly ApplicationDbContext _context;
        public RealEstatesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<RealEstate>> List(int page, int pageSize) // pole kindel mis sisemisesse <> l'heb 05.11
        {
            var result = await _context.RealEstates.GetPagedAsync(page, pageSize: 3);

            return result;
        }
        public async Task<RealEstate> GetById(int id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(m => m.RealEstateId == id);

            return result;
        }
        public async Task Save(RealEstate realEstate)
        {
            if (realEstate.RealEstateId == null)
            {
                _context.Add(realEstate);
            }
            else
            {
                _context.Update(realEstate);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var realEstate = await _context.RealEstates.FindAsync(id);
            if (realEstate != null)
            {
                _context.RealEstates.Remove(realEstate);
            }
            await _context.SaveChangesAsync();
        }

        public bool RealEstateExists(int id)
        {
            return (_context.RealEstates?.Any(e => e.RealEstateId == id)).GetValueOrDefault();

        }

        public async Task<bool> PurchaseRealEstate(PurchaseRealEstatesViewModel model)
        {
            var userFunds = await _context.UserFunds.FirstOrDefaultAsync();
            if (userFunds.Balance < model.PurchasePrice)
            {
                return false;
            }

            //29.11 lisatud peale seda kui transactions tabelit muutsin
            var balanceBefore = userFunds.Balance.Value;

            var newRealEstate = new RealEstate
            {
                RealEstateName = model.RealEstateName,
                RealEstateCountry = model.RealEstateCountry,
                RealEstateCity = model.RealEstateCity,
                RealEstateAddress = model.RealEstateAddress,
                PurchaseDate = DateTime.UtcNow,
                PurchasePrice = model.PurchasePrice,
                CurrentValue = model.PurchasePrice,
                LastCurrentValueChangeTime = DateTime.UtcNow,
                //InvestmentType = "RealEstate", // automaateselt pandud
                CurrentlyOwned = true

            };
            _context.RealEstates.Add(newRealEstate);

            userFunds.Balance -= model.PurchasePrice.Value;// Kodra tehtud viga ili cshtmlis// siin istub mingi viga, mis genereerib nein NULL e
            userFunds.LockedFunds += model.PurchasePrice.Value;

            await _context.SaveChangesAsync(); // see salvestab tehingu mille k'igus genereeritakse uus assetID, mida saab kasutada tehingu [leskirjutamiseks Transactions tabelisse.

            var balanceAfter = userFunds.Balance.Value;
            var lockedFunds = userFunds.LockedFunds.Value;
            var transactionResult = 0;
            var transactedAmount = 1;
                balanceAfter -= model.PurchasePrice.Value;
                lockedFunds += model.PurchasePrice.Value;
                transactionResult = (int)model.PurchasePrice.Value * transactedAmount; //int vs decimal

            var transactionRecord = new Transactions
            {
                //TransactionId should autoincrement 
                TransactionTime = newRealEstate.PurchaseDate,
                InvestmentType = "RealEstate",
                AssetId = newRealEstate.RealEstateId,
                Action = "Purchase",
                BalanceBefore = balanceBefore,
                TransactedAmount = 1,
                TransactionUnitCost = newRealEstate.PurchasePrice.Value,
                TransactionResult = transactionResult,
                LockedFunds = lockedFunds,
                BalanceAfter = balanceAfter, 
                LossOrProfit = 0

            };
            _context.Transactions.Add(transactionRecord);
            await _context.SaveChangesAsync();
            return true;

        }
        //25.11
        public async Task<bool> SellRealEstate(int realEstateId, decimal sellingPrice)
        {
            var realEstate = await _context.RealEstates.FindAsync(realEstateId);
            if (realEstate == null || !realEstate.CurrentlyOwned)
            {
                return false;
            }
            var userFunds = await _context.UserFunds.FirstOrDefaultAsync();

            if (userFunds == null)
            {
                return false;
            }

            userFunds.Balance += sellingPrice;
            userFunds.LockedFunds -= sellingPrice;
            realEstate.CurrentlyOwned = false;

            // realEstate.CurrentValue = sellingPrice;
            var transactionRecord = new Transactions
            {
                TransactionTime = DateTime.UtcNow,
                InvestmentType = "RealEstate",
                AssetId = realEstateId,
                Action = "Sell",
                TransactedAmount = 1,
                TransactionUnitCost = sellingPrice,
                TransactionResult = sellingPrice,

            };

            _context.Transactions.Add(transactionRecord);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}