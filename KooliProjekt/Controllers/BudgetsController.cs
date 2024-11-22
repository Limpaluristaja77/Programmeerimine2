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
    public class BudgetsController : Controller
    {
        private readonly ApplicationDbContext _budgetservice;

        public BudgetsController(ApplicationDbContext context)
        {
            _budgetservice = context;
        }

        // GET: Budgets
        public async Task<IActionResult> Index(int page = 1)
        {
            var applicationDbContext = _budgetservice.Budgets.Include(b => b.Buildings).Include(b => b.Client).Include(b => b.Services);
            return View(await applicationDbContext.GetPagedAsync(page ,5));
        }

        // GET: Budgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _budgetservice.Budgets
                .Include(b => b.Buildings)
                .Include(b => b.Client)
                .Include(b => b.Services)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // GET: Budgets/Create
        public IActionResult Create()
        {
            ViewData["BuildingsId"] = new SelectList(_budgetservice.Buildings, "Id", "Name");
            ViewData["ClientId"] = new SelectList(_budgetservice.Clients, "Id", "Name");
            ViewData["ServicesId"] = new SelectList(_budgetservice.Services, "Id", "Name");
            return View();
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,BuildingsId,ServicesId,Date,Cost")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                _budgetservice.Add(budget);
                await _budgetservice.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingsId"] = new SelectList(_budgetservice.Buildings, "Id", "Name", budget.BuildingsId);
            ViewData["ClientId"] = new SelectList(_budgetservice.Clients, "Id", "Name", budget.ClientId);
            ViewData["ServicesId"] = new SelectList(_budgetservice.Services, "Id", "Name", budget.ServicesId);
            return View(budget);
        }

        // GET: Budgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _budgetservice.Budgets.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            ViewData["BuildingsId"] = new SelectList(_budgetservice.Buildings, "Id", "Name", budget.BuildingsId);
            ViewData["ClientId"] = new SelectList(_budgetservice.Clients, "Id", "Name", budget.ClientId);
            ViewData["ServicesId"] = new SelectList(_budgetservice.Services, "Id", "Name", budget.ServicesId);
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,BuildingsId,ServicesId,Date,Cost")] Budget budget)
        {
            if (id != budget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _budgetservice.Update(budget);
                    await _budgetservice.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(budget.Id))
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
            ViewData["BuildingsId"] = new SelectList(_budgetservice.Buildings, "Id", "Name", budget.BuildingsId);
            ViewData["ClientId"] = new SelectList(_budgetservice.Clients, "Id", "Name", budget.ClientId);
            ViewData["ServicesId"] = new SelectList(_budgetservice.Services, "Id", "Name", budget.ServicesId);
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _budgetservice.Budgets
                .Include(b => b.Buildings)
                .Include(b => b.Client)
                .Include(b => b.Services)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budget = await _budgetservice.Budgets.FindAsync(id);
            if (budget != null)
            {
                _budgetservice.Budgets.Remove(budget);
            }

            await _budgetservice.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetExists(int id)
        {
            return _budgetservice.Budgets.Any(e => e.Id == id);
        }
    }
}
