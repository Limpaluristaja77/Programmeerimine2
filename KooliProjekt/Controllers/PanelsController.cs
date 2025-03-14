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
    public class PanelsController : Controller
    {
        private readonly IPanelsService _panelsservice;

        public PanelsController(IPanelsService panelsService)
        {
            _panelsservice = panelsService;
        }

        // GET: Panels
        public async Task<IActionResult> Index(int page = 1, PanelIndexModel model = null)
        {
            model = model ?? new PanelIndexModel();
            model.Data = await _panelsservice.List(page, 5, model.Search);
            return View(model);
        }

        // GET: Panels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panels = await _panelsservice.Get(id.Value);
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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Panel panels)
        {
            if (ModelState.IsValid)
            {
                await _panelsservice.Save(panels);
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

            var panels = await _panelsservice.Get(id.Value);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Unit,UnitCost,Manufacturer")] Panel panels)
        {
            if (id != panels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _panelsservice.Save(panels);
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

            var panels = await _panelsservice.Get(id.Value);
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
            await _panelsservice.Delete(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
