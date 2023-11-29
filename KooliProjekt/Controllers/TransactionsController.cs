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
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        //PAGER//INDEX INDEX
        // proovin Indexi 'ra muuta selleks, et saaksin n'ha uut transactionsite tabelit. 
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _transactionsService.List(page, 3);
            return View(result);
        }

        //public async Task<IActionResult> Index(int page = 1)
        //{
        //    var result = await _transactionsService.List(page, 5); // pageri osa

        //    var transactionsViewModel = Transactions.Results.Select => new TransactionsViewModel
        //    {

        //    }).ToList();

        //}


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

                await _transactionsService.Save(transactions);
                return RedirectToAction(nameof(Index));
            }
            return View(transactions);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id) // Pole vajalikkuses veendunud, pigem eemaldaks. 
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactions = await _transactionsService.GetById(id.Value);
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

            await _transactionsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
