using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyek2_Bengkel.Data;
using Proyek2_Bengkel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace Proyek2_Bengkel.Controllers
{
    public class LoginController : Controller
    {
        private readonly Proyek2_BengkelContext _context;

        public LoginController(Proyek2_BengkelContext context)
        {
            _context = context;
        }
        public IActionResult Index(String? msg)
        {
            ViewBag.message = msg;
            return View();
        }
        public async Task<IActionResult> LoginAuth([Bind("Username,Password")] Teller teller)
        {
            var getTeller = await _context.Teller.FirstOrDefaultAsync(c => c.Username == teller.Username);
            if (getTeller == null)
            {
                return RedirectToAction("Index", "Login", new { msg = "Username tidak ditemukan!" });
            }

            var checkTeller = new List<Teller>()
            {
                new Teller{Username=getTeller.Username,Password=getTeller.Password}
            };

            var ue = checkTeller.Where(u => u.Username.Equals(teller.Username));

            var up = checkTeller.Where(p => p.Password.Equals(teller.Password));

            if (up.Count() == 1)
            {
                HttpContext.Session.SetString("TellerRole", getTeller.Role);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login", new { msg = "Password Salah!" });
            }
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutValueAsync([Bind("Username,Password")] Teller teller)
        {
            var getTeller = await _context.Teller.FirstOrDefaultAsync(c => c.Username == teller.Username);
            if (getTeller == null)
            {
                ViewBag.message = "Username Tidak Ditemukan!";
                return View();
            }

            var checkTeller = new List<Teller>()
            {
                new Teller{Username=getTeller.Username,Password=getTeller.Password}
            };

            var ue = checkTeller.Where(u => u.Username.Equals(teller.Username));

            var up = checkTeller.Where(p => p.Password.Equals(teller.Password));

            if (up.Count() == 1)
            {
                return View("../Home/Index");
            }
            else
            {
                ViewBag.message = "Password Salah!";
                return View();
            }
        }*/
    }
}
