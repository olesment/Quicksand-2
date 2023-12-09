using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class RealEstatesRepository : IRealEstatesRepository
    {
        //t5 09.12 Toon esialgsest servicest dbcontexti siia selleks et esialgne service ei teaks 
        //dBContextist midagi. 

        private readonly ApplicationDbContext _context;

        public RealEstatesRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        /*
         * 
         * CRUD 
         * 
         */

        //Read
        public async Task<PagedResult<RealEstate>> List(int page, int pageSize)
        {
            var result = await _context.RealEstates.GetPagedAsync(page, pageSize: 3);

            return result;
        }
        //Read
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
                await _context.AddAsync(realEstate); //Await ja async lisatud t5
            }
            else
            {
                _context.Update(realEstate); // siia mingil p]hjusel async ei panda. 
            }
            //await _context.SaveChangesAsync(); // 09.12 gunn kustutas 'ra. Teeb mingiu save meetodi asemele 
            //Pmst lisas esimesse if blokki await ja asynci, mis siis vast peaks 
            //tegema sama mis see Await contextSave changes. 

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


        /*
         * 
         * Peale CRUDI saab siia lisada ja p'ringud, QUERIES
         * Teine Blokk t'iendavate meetodite jaoks. 
         */
        // siit algab omalooming mis ei ole tunniga 1:1 seotud. Seal mingi oma asi, mul oma. 
        //Kui oli vaja seda service puhastada siis ju on vaja ka see asi ringi t]sta. 

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

            userFunds.Balance = balanceBefore - model.PurchasePrice.Value;// Kodra tehtud viga ili cshtmlis// siin istub mingi viga, mis genereerib nein NULL e
            userFunds.LockedFunds += model.PurchasePrice.Value;

            await _context.SaveChangesAsync(); // see salvestab tehingu mille k'igus genereeritakse uus assetID, mida saab kasutada tehingu [leskirjutamiseks Transactions tabelisse.

            var balanceAfter = userFunds.Balance.Value;
            var lockedFunds = userFunds.LockedFunds.Value;
            var transactionResult = 0;
            var transactedAmount = 1;
            //balanceAfter -= model.PurchasePrice.Value;
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
        //25.11 loodud. 09.12 siia toodud. 
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

            var balanceBefore = userFunds.Balance; //Salvestab muutujasse jooksva rahakoti sisu
            await _context.SaveChangesAsync();

            //var balanceAfter = balanceBefore + sellingPrice; 

            userFunds.Balance = balanceBefore + sellingPrice;
            userFunds.LockedFunds -= sellingPrice;
            realEstate.CurrentlyOwned = false;
            
            // realEstate.CurrentValue = sellingPrice;
            var transactionRecord = new Transactions
            {
                TransactionTime = DateTime.UtcNow,
                //transactionID should Autoincrement
                AssetId = realEstateId,
                Action = "Sell",
                BalanceBefore = balanceBefore,
                TransactedAmount = 1,
                TransactionUnitCost = sellingPrice,
                TransactionResult = sellingPrice,
                BalanceAfter = balanceBefore + sellingPrice,
                InvestmentType = "RealEstate", ///asset type === investmen type

            };

            _context.Transactions.Add(transactionRecord);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
