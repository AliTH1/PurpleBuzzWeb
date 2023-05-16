using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.DAL;
using PurpleBuzzWeb.Models;

namespace PurpleBuzzWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RecentWorksController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public RecentWorksController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            ICollection<RecentWork> recentWorks = await _appDbContext.RecentWorks.ToListAsync();
            return View(recentWorks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecentWork recentWork)
        {
            RecentWork recentWork1 = new();

            bool isExists = await _appDbContext.RecentWorks.AnyAsync(x => x.Id == recentWork.Id);

            if (isExists)
            {
                ModelState.AddModelError("Recent Work", "Recent work alredy exists");
                return View();
            }

            recentWork1.CardTitle = recentWork.CardTitle;
            recentWork1.CardText = recentWork.CardText;
            recentWork1.ImagePath = recentWork.ImagePath;

            await _appDbContext.RecentWorks.AddAsync(recentWork1);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            RecentWork recentWork = await _appDbContext.RecentWorks.FindAsync(id);

            if (recentWork == null)
            {
                return NotFound();
            }

            return View(recentWork);
        }

        public async Task<IActionResult> Update(int id, string cardTitle, string cardText,
            string imagePath)
        {
            RecentWork? recentWork = await _appDbContext.RecentWorks.FindAsync(id);

            if (recentWork == null)
            {
                return NotFound();
            }

            recentWork.CardTitle = cardTitle;
            recentWork.CardText = cardText;
            recentWork.ImagePath = imagePath;

            _appDbContext.Update(recentWork);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            RecentWork? recentWork = await _appDbContext.RecentWorks.FindAsync(id);

            if (recentWork == null)
            {
                return NotFound();
            }

            _appDbContext.RecentWorks.Remove(recentWork);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
