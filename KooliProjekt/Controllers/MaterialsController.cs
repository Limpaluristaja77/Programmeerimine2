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
    public class MaterialsController : Controller
    {
        private readonly IMaterialsService _materialsservice;

        public MaterialsController(IMaterialsService materialsService)
        {
            _materialsservice = materialsService;
        }

        // GET: Materials
        public async Task<IActionResult> Index(int page = 1, MaterialIndexModel model = null)
        {
            model = model ?? new MaterialIndexModel();
            model.Data = await _materialsservice.List(page, 5, model.Search);
            return View(model);
        }

        // GET: Materials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials = await _materialsservice.Get(id.Value);
            if (materials == null)
            {
                return NotFound();
            }

            return View(materials);
        }

        // GET: Materials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Material materials)
        {
            if (ModelState.IsValid)
            {
                await _materialsservice.Save(materials);
                return RedirectToAction(nameof(Index));
            }
            return View(materials);
        }

        // GET: Materials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials = await _materialsservice.Get(id.Value);
            if (materials == null)
            {
                return NotFound();
            }
            return View(materials);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Unit,UnitCost,Manufacturer")] Material materials)
        {
            if (id != materials.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _materialsservice.Save(materials);
                return RedirectToAction(nameof(Index));
            }
            return View(materials);
        }

        // GET: Materials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials = await _materialsservice.Get(id.Value);
            if (materials == null)
            {
                return NotFound();
            }

            return View(materials);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _materialsservice.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
