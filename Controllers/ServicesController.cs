using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.DAL;
using PurpleBuzzWeb.Models;

namespace PurpleBuzzWeb.Controllers
{
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;
        public ServicesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.dbServiceCount = await _context.Services.CountAsync();

            return View(
                await _context.Services
                .OrderByDescending(s => s.Id)
                .Where(c => !c.IsDeleted)
                .Take(8)
                .Include(c => c.ServiceImages)
                .ToListAsync()
                );
        }

        public async Task<IActionResult> LoadMore(byte skip = 0, byte take = 8)
        {

            List<Service> services = await _context.Services
                .OrderByDescending(s => s.Id)
                .Where(s => !s.IsDeleted)
                .Skip(skip)
                .Take(take)
                .Include(c => c.ServiceImages)
                .ToListAsync();

            return PartialView("_ServicesPartialView", services);
        }
    }
}
