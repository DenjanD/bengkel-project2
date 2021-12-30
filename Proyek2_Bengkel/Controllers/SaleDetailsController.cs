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
using System.Diagnostics;

namespace Proyek2_Bengkel.Controllers
{
    public class SaleDetailsController : Controller
    {
        private readonly Proyek2_BengkelContext _context;

        public SaleDetailsController(Proyek2_BengkelContext context)
        {
            _context = context;
        }

        // GET: SaleDetails
        public async Task<IActionResult> Index()
        {
            var proyek2_BengkelContext = _context.SaleDetail.Include(s => s.Sale).Include(s => s.SparePart);
            return View(await proyek2_BengkelContext.ToListAsync());
        }

        // GET: SaleDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetail
                .Include(s => s.Sale)
                .Include(s => s.SparePart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // GET: SaleDetails/Create
        public IActionResult Create()
        {
            ViewData["SaleId"] = new SelectList(_context.Sale, "Id", "Id");
            ViewData["SparePartId"] = new SelectList(_context.SparePart, "Id", "Id");
            return View();
        }

        // POST: SaleDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SaleId,SparePartId,Qty")] SaleDetail saleDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleDetail);
                await _context.SaveChangesAsync();

                var getSale = await _context.SparePart.FirstOrDefaultAsync(c => c.Id == saleDetail.SparePartId);
                int totalCost = getSale.Price * saleDetail.Qty;

                var getSaleUpdate = await _context.Sale.FirstOrDefaultAsync(d => d.Id == saleDetail.SaleId);
                getSaleUpdate.TotalCost = totalCost;

                _context.Entry(getSaleUpdate).CurrentValues.SetValues(getSaleUpdate);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            ViewData["SaleId"] = new SelectList(_context.Sale, "Id", "Id", saleDetail.SaleId);
            ViewData["SparePartId"] = new SelectList(_context.SparePart, "Id", "Id", saleDetail.SparePartId);

            return View(saleDetail);
        }

        // GET: SaleDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetail.FindAsync(id);
            if (saleDetail == null)
            {
                return NotFound();
            }
            ViewData["SaleId"] = new SelectList(_context.Sale, "Id", "Id", saleDetail.SaleId);
            ViewData["SparePartId"] = new SelectList(_context.SparePart, "Id", "Id", saleDetail.SparePartId);
            return View(saleDetail);
        }

        // POST: SaleDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SaleId,SparePartId,Qty")] SaleDetail saleDetail)
        {
            if (id != saleDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleDetail);
                    await _context.SaveChangesAsync();

                    var getSale = await _context.SparePart.FirstOrDefaultAsync(c => c.Id == saleDetail.SparePartId);
                    int totalCost = getSale.Price * saleDetail.Qty;

                    var getSaleUpdate = await _context.Sale.FirstOrDefaultAsync(d => d.Id == saleDetail.SaleId);
                    getSaleUpdate.TotalCost = totalCost;

                    _context.Entry(getSaleUpdate).CurrentValues.SetValues(getSaleUpdate);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleDetailExists(saleDetail.Id))
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
            ViewData["SaleId"] = new SelectList(_context.Sale, "Id", "Id", saleDetail.SaleId);
            ViewData["SparePartId"] = new SelectList(_context.SparePart, "Id", "Id", saleDetail.SparePartId);
            return View(saleDetail);
        }

        // GET: SaleDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetail
                .Include(s => s.Sale)
                .Include(s => s.SparePart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // POST: SaleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleDetail = await _context.SaleDetail.FindAsync(id);
            _context.SaleDetail.Remove(saleDetail);
            await _context.SaveChangesAsync();

            var getSale = await _context.SparePart.FirstOrDefaultAsync(c => c.Id == saleDetail.SparePartId);
            int totalCost = getSale.Price * saleDetail.Qty;

            var getSaleUpdate = await _context.Sale.FirstOrDefaultAsync(d => d.Id == saleDetail.SaleId);
            getSaleUpdate.TotalCost -= totalCost;

            _context.Entry(getSaleUpdate).CurrentValues.SetValues(getSaleUpdate);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool SaleDetailExists(int id)
        {
            return _context.SaleDetail.Any(e => e.Id == id);
        }
    }
}
