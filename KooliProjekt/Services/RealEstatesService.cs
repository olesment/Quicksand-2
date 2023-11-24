using KooliProjekt.Data; //loodud 05.11 T3 raames. 
using KooliProjekt.Models;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Xml;

namespace KooliProjekt.Services
{
    public class RealEstatesService: IRealEstatesService
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
            if(realEstate.RealEstateId == null)
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
       
        public async Task<bool>PurchaseRealEstate(PurchaseRealEstatesViewModel model)
        {
            var userFunds = await _context.UserFunds.FirstOrDefaultAsync();
            if(userFunds.Balance < model.PurchasePrice)
            {
                return false;
            }
            
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
                    //InvestmentType = "RealEstate",
                    CurrentlyOwned = true

             };
            _context.RealEstates.Add(newRealEstate);

            userFunds.Balance -= model.PurchasePrice.Value; // siin istub mingi viga, mis genereerib nein NULL e
            userFunds.LockedFunds += model.PurchasePrice.Value;

            await _context.SaveChangesAsync(); // see salvestab tehingu mille k'igus genereeritakse uus assetID, mida saab kasutada tehingu [leskirjutamiseks Transactions tabelisse.

            var transactionRecord = new Transactions
            {
                TransactionTime =newRealEstate.PurchaseDate,
                InvestmentType = "RealEstate",
                AssetId = newRealEstate.RealEstateId,
                Action = "Purchase",
                TransactedAmount = 1,
                TransactionUnitCost = newRealEstate.PurchasePrice.Value,

            };
            _context.Transactions.Add(transactionRecord);
            await _context.SaveChangesAsync();
            return true;    
                   
        }
    }   
}
