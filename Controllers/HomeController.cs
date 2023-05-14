using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.DAL;
using PurpleBuzzWeb.Models;
using PurpleBuzzWeb.ViewModels;

namespace PurpleBuzzWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {

            HomeVM homeVM = new HomeVM()
            {
                Sliders = await _appDbContext.Sliders.ToListAsync(),
                Categories = await _appDbContext.Categories.Where(c => !c.IsDeleted).ToListAsync(),
                Services = await _appDbContext.Services
                .Include(s => s.Category)
                .Include(s => s.ServiceImages)
                .OrderByDescending(s => s.Id)
                .Where(s => !s.IsDeleted)
                .Take(8)
                .ToListAsync()
            };
            return View(homeVM);
        }


    }
}
