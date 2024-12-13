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
    public class BuildingsController : Controller
    {
        private readonly IBuildingsService _buildingsservice;

        public BuildingsController(IBuildingsService buildingsService)
        {
            _buildingsservice = buildingsService;
        }

        // GET: Buildings
        public async Task<IActionResult> Index(int page = 1, BuildingIndexModel model = null)
        {
            model = model ?? new BuildingIndexModel();
            model.Data = await _buildingsservice.List(page, 5, model.Search);
            return View(model);
        }

        // GET: Buildings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildings = await _buildingsservice.Get(id.Value);
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
                await _buildingsservice.Save(buildings);
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

            var buildings = await _buildingsservice.Get(id.Value);
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
                await _buildingsservice.Save(buildings);
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

            var buildings = await _buildingsservice.Get(id.Value);
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
            await _buildingsservice.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
