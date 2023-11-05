using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Services;

namespace KooliProjekt.Controllers
{
    public class RealEstatesController : Controller
    {
        //private readonly ApplicationDbContext _context; eemaldatud 05.11.t3 raames
        private readonly IRealEstatesService _realEstatesService; // lisatid 05.11 t3 raames selleks et service hakkaks t;;le l2bi program cs builderi. 

        //See annab ligipääsu andmebaasile opereerimiseks seal. 
        public RealEstatesController(/*ApplicationDbContext context,*/ IRealEstatesService realEstatesService)
        /*lisatud IrealestateService + realestateService selleks et dbkontekstist vaikselt loobuda. 05.11. t3 raames.*/ // Selle võtame ära ja paneme RealEstatesService alla 05.11
        {
           // _context = context;
            
            _realEstatesService = realEstatesService;
            /*controller tohib n2ha ainult service interfacei. lisatud 05.11. t3 raames. 1:46*/
        }

        // GET: RealEstates// INDEX MEETOD
        public async Task<IActionResult> Index(int page = 1) //31.10 lisati parameeter page // Kui Realestates tabel ei ole tühi siis annab selles olevad read välja
        {
            var result = await _realEstatesService.List(page, 3);
            /*await _context.RealEstates.GetPagedAsync(page, pageSize: 3); 
             * // eemaldatud ja topitud servisesse ja selle interfacei asemele see mis kirjas. 05.11. t3 raames.*/

            return View(result); 
            //return _context.RealEstates != null ? 
            //              View(/*await _context.RealEstates.ToListAsync()*/) : // see on laiendusmeetod iseenesest 31.10 // viisin Ylemisesse VARi var result. 
            //              Problem("Entity set 'ApplicationDbContext.RealEstates'  is null.");
        }

        // GET: RealEstates/Details/5
        public async Task<IActionResult> Details(int? id) // See vastutab details nupu eest toob välja spetsiifilise real estatei detailid. 
        {
            if (id == null /*|| _context.RealEstates == null*/)
            {
                return NotFound();
            }

            var realEstate = await _realEstatesService.GetById(id.Value);
            /*muudetud 05.11. t3 raames.                
                /*_context.RealEstates
                .FirstOrDefaultAsync(m => m.RealEstateId == id); asendatud 05.11. t3 raames, viidud service ja interface alla. */
            if (realEstate == null)
            {
                return NotFound();
            }

            return View(realEstate);
        }

        // GET: RealEstates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RealEstates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RealEstateId,RealEstateName,RealEstateCountry,RealEstateCity,RealEstateAddress,PurchaseDate,PurchasePrice,CurrentValue,LastCurrentValueChangeTime,CurrentlyOwned")] RealEstate realEstate)
        {
            if (ModelState.IsValid)
            {
                return View(realEstate);
                ///* asendatud ja viidud Service ja selle interface alla 05.11 t3 raames. 
                ///_context.Add(realEstate);*/
               // await _context.SaveChangesAsync();*/
            }
            
            await _realEstatesService.Save(realEstate);
            return RedirectToAction(nameof(Index));
        }

        // GET: RealEstates/Edit/5 //REDIGEERIMISVORM
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null /*|| _context.RealEstates == null*/) //osa v2ljakommenteeritud vastavalt t3 tunnile 1:59 05.11
            {
                return NotFound();
            }

            var realEstate = await _realEstatesService.GetById(id.Value);// _context.RealEstates.FindAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }
            return View(realEstate);
        }

        // POST: RealEstates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] 

        //See salvestab SALVESTAMINE
        public async Task<IActionResult> Edit(int id, [Bind("RealEstateId,RealEstateName,RealEstateCountry,RealEstateCity,RealEstateAddress,PurchaseDate,PurchasePrice,CurrentValue,LastCurrentValueChangeTime,CurrentlyOwned")] RealEstate realEstate)
        {
            if (id != realEstate.RealEstateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                return View(realEstate);
                //try
                //{
                //    _context.Update(realEstate);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!RealEstateExists(realEstate.RealEstateId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}

            }
            await _realEstatesService.Save(realEstate);
            return RedirectToAction(nameof(Index));
            
        }

        // GET: RealEstates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null /*|| _context.RealEstates == null*/)
            {
                return NotFound();
            }

            var realEstate = await _realEstatesService.GetById(id.Value); //05.11 t3. 
                /*_context.RealEstates
                .FirstOrDefaultAsync(m => m.RealEstateId == id);*/
            if (realEstate == null)
            {
                return NotFound();
            }

            return View(realEstate);
        }

        // POST: RealEstates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.RealEstates == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.RealEstates'  is null.");
            //}
            //mahav6etud sest kontekst l2heb maha
            //var realEstate = await _context.RealEstates.FindAsync(id);
            //if (realEstate != null)
            //{
            //    _context.RealEstates.Remove(realEstate);
            //}

            //await _context.SaveChangesAsync();
            await _realEstatesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RealEstateExists(int id)
        {
          return (_context.RealEstates?.Any(e => e.RealEstateId == id)).GetValueOrDefault();
        }
    }
}
