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
    public class BuildingsController : Controller
    {
        private readonly ApplicationDbContext _buildingsservice;

        public BuildingsController(ApplicationDbContext context)
        {
            _buildingsservice = context;
        }

        // GET: Buildings
        public async Task<IActionResult> Index()
        {
            return View(await _buildingsservice.Buildings.ToListAsync());
        }

        // GET: Buildings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildings = await _buildingsservice.Buildings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buildings == null)
            {
                return NotFound();
            }

            return View(buildings);
        }

        // GET: Buildings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PanelId,MaterialId")] Buildings buildings)
        {
            if (ModelState.IsValid)
            {
                _buildingsservice.Add(buildings);
                await _buildingsservice.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buildings);
        }

        // GET: Buildings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildings = await _buildingsservice.Buildings.FindAsync(id);
            if (buildings == null)
            {
                return NotFound();
            }
            return View(buildings);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PanelId,MaterialId")] Buildings buildings)
        {
            if (id != buildings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _buildingsservice.Update(buildings);
                    await _buildingsservice.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingsExists(buildings.Id))
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
            return View(buildings);
        }

        // GET: Buildings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildings = await _buildingsservice.Buildings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buildings == null)
            {
                return NotFound();
            }

            return View(buildings);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildings = await _buildingsservice.Buildings.FindAsync(id);
            if (buildings != null)
            {
                _buildingsservice.Buildings.Remove(buildings);
            }

            await _buildingsservice.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildingsExists(int id)
        {
            return _buildingsservice.Buildings.Any(e => e.Id == id);
        }
    }
}
