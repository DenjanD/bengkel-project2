#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyek2_Bengkel.Data;
using Proyek2_Bengkel.Models;

namespace Proyek2_Bengkel.Controllers
{
    public class PartCategoriesController : Controller
    {
        private readonly Proyek2_BengkelContext _context;

        public PartCategoriesController(Proyek2_BengkelContext context)
        {
            _context = context;
        }

        // GET: PartCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.PartCategory.ToListAsync());
        }

        // GET: PartCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partCategory = await _context.PartCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partCategory == null)
            {
                return NotFound();
            }

            return View(partCategory);
        }

        // GET: PartCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PartCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PartCategory partCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(partCategory);
        }

        // GET: PartCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partCategory = await _context.PartCategory.FindAsync(id);
            if (partCategory == null)
            {
                return NotFound();
            }
            return View(partCategory);
        }

        // POST: PartCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PartCategory partCategory)
        {
            if (id != partCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartCategoryExists(partCategory.Id))
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
            return View(partCategory);
        }

        // GET: PartCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partCategory = await _context.PartCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partCategory == null)
            {
                return NotFound();
            }

            return View(partCategory);
        }

        // POST: PartCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partCategory = await _context.PartCategory.FindAsync(id);
            _context.PartCategory.Remove(partCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartCategoryExists(int id)
        {
            return _context.PartCategory.Any(e => e.Id == id);
        }
    }
}
