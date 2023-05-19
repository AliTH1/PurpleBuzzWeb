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
                Categories = await _appDbContext.Categories.Where(c => !c.IsDeleted).ToListAsync(),
                RecentWorks = await _appDbContext.RecentWorks.ToListAsync()
            };
            return View(homeVM);
        }

    }
}
