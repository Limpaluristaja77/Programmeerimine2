﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public BudgetsController(ApplicationDbContext context,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: Budgets
        public async Task<IActionResult> Index(int page = 1)
        {
            var applicationDbContext = _context.Budgets.Include(b => b.Buildings).Include(b => b.Client).Include(b => b.Services);
            return View(await applicationDbContext.GetPagedAsync(page ,5));
        }

        // GET: Budgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgets
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
            ViewData["BuildingsId"] = new SelectList(_context.Buildings, "Id", "Name");
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
            ViewData["ServicesId"] = new SelectList(_context.Services, "Id", "Name");
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


                await _unitOfWork.BeginTransaction();
                try
                {
                    await _unitOfWork.BudgetRepository.Save(budget);

                 

                    await _unitOfWork.Commit();
                }
                catch
                {
                    await _unitOfWork.Rollback();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(budget);
        }

        // GET: Budgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            ViewData["BuildingsId"] = new SelectList(_context.Buildings, "Id", "Name", budget.BuildingsId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", budget.ClientId);
            ViewData["ServicesId"] = new SelectList(_context.Services, "Id", "Name", budget.ServicesId);
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
                    _context.Update(budget);
                    await _context.SaveChangesAsync();
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
            ViewData["BuildingsId"] = new SelectList(_context.Buildings, "Id", "Name", budget.BuildingsId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", budget.ClientId);
            ViewData["ServicesId"] = new SelectList(_context.Services, "Id", "Name", budget.ServicesId);
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgets
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
            var budget = await _context.Budgets.FindAsync(id);
            if (budget != null)
            {
                _context.Budgets.Remove(budget);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetExists(int id)
        {
            return _context.Budgets.Any(e => e.Id == id);
        }
    }
}
