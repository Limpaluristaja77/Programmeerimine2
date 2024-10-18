﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class PanelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PanelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Panels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Panels.ToListAsync());
        }

        // GET: Panels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panels = await _context.Panels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panels == null)
            {
                return NotFound();
            }

            return View(panels);
        }

        // GET: Panels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Panels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Unit,UnitCost,Manufacturer")] Panels panels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(panels);
        }

        // GET: Panels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panels = await _context.Panels.FindAsync(id);
            if (panels == null)
            {
                return NotFound();
            }
            return View(panels);
        }

        // POST: Panels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Unit,UnitCost,Manufacturer")] Panels panels)
        {
            if (id != panels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanelsExists(panels.Id))
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
            return View(panels);
        }

        // GET: Panels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panels = await _context.Panels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panels == null)
            {
                return NotFound();
            }

            return View(panels);
        }

        // POST: Panels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var panels = await _context.Panels.FindAsync(id);
            if (panels != null)
            {
                _context.Panels.Remove(panels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanelsExists(int id)
        {
            return _context.Panels.Any(e => e.Id == id);
        }
    }
}