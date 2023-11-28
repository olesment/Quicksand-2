using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Models;//vajalik service kasutamiseks

namespace KooliProjekt.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionsService _transactionsService
            ;
        //private readonly ApplicationDbContext _context;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        //PAGER//INDEX INDEX
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _transactionsService.List(page, 3);
            return View(result);
        }

        // GET: Transactions
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Transactions != null ? 
        //                  View(await _context.Transactions.ToListAsync()) :
        //                  Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
        //}

        // GET: Transactions/Details/5
        //DETAILS WITH GET BY ID --------------------------------!
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var transaction = await _transactionsService.GetById(id.Value);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Transactions == null)
        //    {
        //        return NotFound();
        //    }

        //    var transactions = await _context.Transactions
        //        .FirstOrDefaultAsync(m => m.TransactionId == id);
        //    if (transactions == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(transactions);
        //}

        // GET: Transactions/Create
        public IActionResult Create() // 28.11 pole tegelt kindel kas seda siia yldse vaja on. 
        {
            return View();
        }

        // POST: Transactions/Create //28.11 muudetud. POLE TEGELIKULT KINDEL KAS SEDA VAJA ON JA KAS J"TTA!
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,TransactionTime,InvestmentType,AssetId,Action,TransactedAmount,TransactionUnitCost,TransactionResult")] Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                await _transactionsService.Save(transactions); //28.11 siin v]ib olla mingi jura transaction vs transactions
                return RedirectToAction(nameof(Index));
                //_context.Add(transactions);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(transactions); // 28.11 sama jama nimedega 
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null /*|| _context.Transactions == null*/)
            {
                return NotFound();
            }

            var transaction = await _transactionsService.GetById(id.Value);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,TransactionTime,InvestmentType,AssetId,Action,TransactedAmount,TransactionUnitCost,TransactionResult")] Transactions transactions)
        {
            if (id != transactions.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(transactions);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!TransactionsExists(transactions.TransactionId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                await _transactionsService.Save(transactions);
                return RedirectToAction(nameof(Index));
            }
            return View(transactions);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null /*|| _context.Transactions == null*/)
            {
                return NotFound();
            }

            var transactions = await _transactionsService.GetById(id.Value);
                //.FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactions == null)
            {
                return NotFound();
            }

            return View(transactions);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.Transactions == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            //}
            //var transactions = await _context.Transactions.FindAsync(id);
            //if (transactions != null)
            //{
            //    _context.Transactions.Remove(transactions);
            //}

            //await _context.SaveChangesAsync();
            await _transactionsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool TransactionsExists(int id)
        //{
        //  return (_context.Transactions?.Any(e => e.TransactionId == id)).GetValueOrDefault();
        //}
    }
}
