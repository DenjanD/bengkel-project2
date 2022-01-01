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
    public class ServicesController : Controller
    {
        private readonly Proyek2_BengkelContext _context;

        public ServicesController(Proyek2_BengkelContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var proyek2_BengkelContext = _context.Service.Include(s => s.Customer).Include(s => s.ServiceCategory);
            return View(await proyek2_BengkelContext.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Service
                .Include(s => s.Customer)
                .Include(s => s.ServiceCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id");
            ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategory, "Id", "Id");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,ServiceCategoryId,Complaint,VehicleName,VehiclePlate,ServiceDate,ServiceCost")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();

                var getServiceCategory = await _context.ServiceCategory.FirstOrDefaultAsync(c => c.Id == service.ServiceCategoryId);
                int totalCost = getServiceCategory.Cost;

                var getServiceUpdate = await _context.Service.FirstOrDefaultAsync(d => d.Id == service.Id);
                getServiceUpdate.ServiceCost += totalCost;

                _context.Entry(getServiceUpdate).CurrentValues.SetValues(getServiceUpdate);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", service.CustomerId);
            ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategory, "Id", "Id", service.ServiceCategoryId);
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Service.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", service.CustomerId);
            ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategory, "Id", "Id", service.ServiceCategoryId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,ServiceCategoryId,Complaint,VehicleName,VehiclePlate,ServiceDate,ServiceCost")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();

                    var getServiceCategory = await _context.ServiceCategory.FirstOrDefaultAsync(c => c.Id == service.ServiceCategoryId);
                    int newCost = getServiceCategory.Cost;

                    var getServiceUpdate = await _context.Service.FirstOrDefaultAsync(d => d.Id == service.Id);
                    getServiceUpdate.ServiceCost += newCost;

                    _context.Entry(getServiceUpdate).CurrentValues.SetValues(getServiceUpdate);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", service.CustomerId);
            ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategory, "Id", "Id", service.ServiceCategoryId);
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Service
                .Include(s => s.Customer)
                .Include(s => s.ServiceCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Service.FindAsync(id);
            _context.Service.Remove(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Service.Any(e => e.Id == id);
        }
    }
}
