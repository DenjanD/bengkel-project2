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
    public class ServiceDetailsController : Controller
    {
        private readonly Proyek2_BengkelContext _context;

        public ServiceDetailsController(Proyek2_BengkelContext context)
        {
            _context = context;
        }

        // GET: ServiceDetails
        public async Task<IActionResult> Index()
        {
            var proyek2_BengkelContext = _context.ServiceDetail.Include(s => s.Service).Include(s => s.SparePart);
            return View(await proyek2_BengkelContext.ToListAsync());
        }

        // GET: ServiceDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceDetail = await _context.ServiceDetail
                .Include(s => s.Service)
                .Include(s => s.SparePart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceDetail == null)
            {
                return NotFound();
            }

            return View(serviceDetail);
        }

        // GET: ServiceDetails/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Service, "Id", "Id");
            ViewData["SparePartId"] = new SelectList(_context.SparePart, "Id", "Id");
            return View();
        }

        // POST: ServiceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServiceId,SparePartId,PartId")] ServiceDetail serviceDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Service, "Id", "Id", serviceDetail.ServiceId);
            return View(serviceDetail);
        }

        // GET: ServiceDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceDetail = await _context.ServiceDetail.FindAsync(id);
            if (serviceDetail == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.Service, "Id", "Id", serviceDetail.ServiceId);
            ViewData["SparePartId"] = new SelectList(_context.SparePart, "Id", "Id", serviceDetail.SparePartId);
            return View(serviceDetail);
        }

        // POST: ServiceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServiceId,SparePartId,PartId")] ServiceDetail serviceDetail)
        {
            if (id != serviceDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceDetailExists(serviceDetail.Id))
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
            ViewData["ServiceId"] = new SelectList(_context.Service, "Id", "Id", serviceDetail.ServiceId);
            return View(serviceDetail);
        }

        // GET: ServiceDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceDetail = await _context.ServiceDetail
                .Include(s => s.Service)
                .Include(s => s.SparePart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceDetail == null)
            {
                return NotFound();
            }

            return View(serviceDetail);
        }

        // POST: ServiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceDetail = await _context.ServiceDetail.FindAsync(id);
            _context.ServiceDetail.Remove(serviceDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceDetailExists(int id)
        {
            return _context.ServiceDetail.Any(e => e.Id == id);
        }
    }
}
