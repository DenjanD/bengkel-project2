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
    public class ServiceCategoriesController : Controller
    {
        private readonly Proyek2_BengkelContext _context;

        public ServiceCategoriesController(Proyek2_BengkelContext context)
        {
            _context = context;
        }

        // GET: ServiceCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceCategory.ToListAsync());
        }

        // GET: ServiceCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCategory = await _context.ServiceCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceCategory == null)
            {
                return NotFound();
            }

            return View(serviceCategory);
        }

        // GET: ServiceCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cost")] ServiceCategory serviceCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceCategory);
        }

        // GET: ServiceCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCategory = await _context.ServiceCategory.FindAsync(id);
            if (serviceCategory == null)
            {
                return NotFound();
            }
            return View(serviceCategory);
        }

        // POST: ServiceCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cost")] ServiceCategory serviceCategory)
        {
            if (id != serviceCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceCategoryExists(serviceCategory.Id))
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
            return View(serviceCategory);
        }

        // GET: ServiceCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCategory = await _context.ServiceCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceCategory == null)
            {
                return NotFound();
            }

            return View(serviceCategory);
        }

        // POST: ServiceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceCategory = await _context.ServiceCategory.FindAsync(id);
            _context.ServiceCategory.Remove(serviceCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceCategoryExists(int id)
        {
            return _context.ServiceCategory.Any(e => e.Id == id);
        }
    }
}
