using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class UserFundsTransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserFundsTransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserFundsTransactions
        public async Task<IActionResult> Index()
        {
              return _context.UserFundsTransactions != null ? 
                          View(await _context.UserFundsTransactions.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.UserFundsTransactions'  is null.");
        }

        // GET: UserFundsTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserFundsTransactions == null)
            {
                return NotFound();
            }

            var userFundsTransaction = await _context.UserFundsTransactions
                .FirstOrDefaultAsync(m => m.FundsTransactionId == id);
            if (userFundsTransaction == null)
            {
                return NotFound();
            }

            return View(userFundsTransaction);
        }

        // GET: UserFundsTransactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserFundsTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FundsTransactionId,FundID,Amount,TransactionDate,TransactionType,Comment")] UserFundsTransaction userFundsTransaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userFundsTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userFundsTransaction);
        }

        // GET: UserFundsTransactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserFundsTransactions == null)
            {
                return NotFound();
            }

            var userFundsTransaction = await _context.UserFundsTransactions.FindAsync(id);
            if (userFundsTransaction == null)
            {
                return NotFound();
            }
            return View(userFundsTransaction);
        }

        // POST: UserFundsTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FundsTransactionId,FundID,Amount,TransactionDate,TransactionType,Comment")] UserFundsTransaction userFundsTransaction)
        {
            if (id != userFundsTransaction.FundsTransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFundsTransaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFundsTransactionExists(userFundsTransaction.FundsTransactionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userFundsTransaction);
        }

        // GET: UserFundsTransactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserFundsTransactions == null)
            {
                return NotFound();
            }

            var userFundsTransaction = await _context.UserFundsTransactions
                .FirstOrDefaultAsync(m => m.FundsTransactionId == id);
            if (userFundsTransaction == null)
            {
                return NotFound();
            }

            return View(userFundsTransaction);
        }

        // POST: UserFundsTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserFundsTransactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserFundsTransactions'  is null.");
            }
            var userFundsTransaction = await _context.UserFundsTransactions.FindAsync(id);
            if (userFundsTransaction != null)
            {
                _context.UserFundsTransactions.Remove(userFundsTransaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFundsTransactionExists(int id)
        {
          return (_context.UserFundsTransactions?.Any(e => e.FundsTransactionId == id)).GetValueOrDefault();
        }
    }
}
