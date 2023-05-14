using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.DAL;
using PurpleBuzzWeb.Models;

namespace PurpleBuzzWeb.Controllers
{
    public class WorkController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public WorkController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<WorkCategory> workCategories = await _appDbContext.WorkCategories.ToListAsync();
            return View(workCategories);
        }
    }
}
