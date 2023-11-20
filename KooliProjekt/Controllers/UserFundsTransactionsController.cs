using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Models;

namespace KooliProjekt.Controllers
{
    public class UserFundsTransactionsController : Controller
    {
       // private readonly ApplicationDbContext _context;
        private readonly IUserFundsTransactionsService _userFundsTransactionsService;

        //public UserFundsTransactionsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        public UserFundsTransactionsController(IUserFundsTransactionsService userFundsTransactionService)
        {
            _userFundsTransactionsService = userFundsTransactionService;
        }

        // GET: UserFundsTransactions
        //public async Task<IActionResult> Index()
        //{
        //      return _context.UserFundsTransactions != null ? 
        //                  View(await _context.UserFundsTransactions.ToListAsync()) :
        //                  Problem("Entity set 'ApplicationDbContext.UserFundsTransactions'  is null.");
        //}

        //public async Task<IActionResult> Index(int page = 1)
        //{
        //    var result = await _userFundsTransactionsService.List(page, 3);
        //    return View(result);
        //}

        //// GET: UserFundsTransactions/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.UserFundsTransactions == null)
        //    {
        //        return NotFound();
        //    }

        //    var userFundsTransaction = await _context.UserFundsTransactions
        //        .FirstOrDefaultAsync(m => m.FundsTransactionId == id);
        //    if (userFundsTransaction == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userFundsTransaction);
        //}
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _userFundsTransactionsService.List(page, 3);
            return View(result);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFundsTransaction = await _userFundsTransactionsService.GetById(id.Value);

            if (userFundsTransaction == null)
            {
                return NotFound();
            }

            return View(userFundsTransaction);
        }

        // GET: UserFundsTransactions/Create // siin pole eraldi createi vaja 
        //public IActionResult Create()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult Deposit() // see renderdab view koos formiga selleks et raha peale kanda.
        {
            return View(new DepositViewModel());
        }
        [HttpGet]
        public IActionResult Withdraw() // see renderdab view koos formiga, et raha v2lja v6tta
        {
            return View(new WithdrawViewModel());
        }
        // POST: UserFundsTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("FundsTransactionId,FundID,Amount,TransactionDate,TransactionType,Comment")] UserFundsTransaction userFundsTransaction)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(userFundsTransaction);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userFundsTransaction);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(DepositViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _userFundsTransactionsService.Deposit(model.FundID, model.Amount, model.Comment);
                if (success)
                {
                    return RedirectToAction("Index", "UserFundsTransactions");
                }
                else
                {
                    return View(model); // ma ei tea mida see t'pselt teeb. 
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(WithdrawViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _userFundsTransactionsService.Withdraw(model.FundID, model.Amount, model.Comment);
                if (success)
                {
                    return RedirectToAction("Index", "UserFundsTransactions");
                }
                else
                {
                    return View(model); // ma ei tea mida see t'pselt teeb. 
                }
            }
            return View(model);
        }
     

        // GET: UserFundsTransactions/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.UserFundsTransactions == null)
        //    {
        //        return NotFound();
        //    }

        //    var userFundsTransaction = await _context.UserFundsTransactions.FindAsync(id);
        //    if (userFundsTransaction == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(userFundsTransaction);
        //}

        // POST: UserFundsTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("FundsTransactionId,FundID,Amount,TransactionDate,TransactionType,Comment")] UserFundsTransaction userFundsTransaction)
        //{
        //    if (id != userFundsTransaction.FundsTransactionId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(userFundsTransaction);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserFundsTransactionExists(userFundsTransaction.FundsTransactionId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userFundsTransaction);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("FundsTransactionId,FundID,Amount,TransactionDate,TransactionType,Comment")] UserFundsTransaction userFundsTransaction)
        //{
        //    if (id != userFundsTransaction.FundsTransactionId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(userFundsTransaction);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserFundsTransactionExists(userFundsTransaction.FundsTransactionId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userFundsTransaction);
        //}

        // GET: UserFundsTransactions/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.UserFundsTransactions == null)
        //    {
        //        return NotFound();
        //    }

        //    var userFundsTransaction = await _context.UserFundsTransactions
        //        .FirstOrDefaultAsync(m => m.FundsTransactionId == id);
        //    if (userFundsTransaction == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userFundsTransaction);
        //}

        //// POST: UserFundsTransactions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.UserFundsTransactions == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.UserFundsTransactions'  is null.");
        //    }
        //    var userFundsTransaction = await _context.UserFundsTransactions.FindAsync(id);
        //    if (userFundsTransaction != null)
        //    {
        //        _context.UserFundsTransactions.Remove(userFundsTransaction);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserFundsTransactionExists(int id)
        //{
        //  return (_context.UserFundsTransactions?.Any(e => e.FundsTransactionId == id)).GetValueOrDefault();
        //}
    }
}
