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
    public class SparePartsController : Controller
    {
        private readonly Proyek2_BengkelContext _context;

        public SparePartsController(Proyek2_BengkelContext context)
        {
            _context = context;
        }

        // GET: SpareParts
        public async Task<IActionResult> Index()
        {
            var proyek2_BengkelContext = _context.SparePart.Include(s => s.PartCategory).Include(s => s.Supplier);
            return View(await proyek2_BengkelContext.ToListAsync());
        }

        // GET: SpareParts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SparePart
                .Include(s => s.PartCategory)
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sparePart == null)
            {
                return NotFound();
            }

            return View(sparePart);
        }

        // GET: SpareParts/Create
        public IActionResult Create()
        {
            ViewData["PartCategoryName"] = new SelectList(_context.PartCategory, "Id", "Name");
            ViewData["SupplierName"] = new SelectList(_context.Supplier, "Id", "Name");
            return View();
        }

        // POST: SpareParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PartCategoryId,SupplierId,Name,Price")] SparePart sparePart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sparePart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartCategoryId"] = new SelectList(_context.PartCategory, "Id", "Id", sparePart.PartCategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Id", sparePart.SupplierId);
            return View(sparePart);
        }

        // GET: SpareParts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SparePart.FindAsync(id);
            if (sparePart == null)
            {
                return NotFound();
            }
            ViewData["PartCategoryName"] = new SelectList(_context.PartCategory, "Id", "Name", sparePart.PartCategoryId);
            ViewData["SupplierName"] = new SelectList(_context.Supplier, "Id", "Name", sparePart.SupplierId);
            return View(sparePart);
        }

        // POST: SpareParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PartCategoryId,SupplierId,Name,Price")] SparePart sparePart)
        {
            if (id != sparePart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sparePart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SparePartExists(sparePart.Id))
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
            ViewData["PartCategoryId"] = new SelectList(_context.PartCategory, "Id", "Id", sparePart.PartCategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Id", sparePart.SupplierId);
            return View(sparePart);
        }

        // GET: SpareParts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SparePart
                .Include(s => s.PartCategory)
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sparePart == null)
            {
                return NotFound();
            }

            return View(sparePart);
        }

        // POST: SpareParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sparePart = await _context.SparePart.FindAsync(id);
            _context.SparePart.Remove(sparePart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SparePartExists(int id)
        {
            return _context.SparePart.Any(e => e.Id == id);
        }
    }
}
