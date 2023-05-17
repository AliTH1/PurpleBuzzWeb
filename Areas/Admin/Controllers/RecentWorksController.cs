using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.Areas.ViewModels;
using PurpleBuzzWeb.DAL;
using PurpleBuzzWeb.Models;
using PurpleBuzzWeb.Utilities.Extensions;

namespace PurpleBuzzWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RecentWorksController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RecentWorksController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create(RecentWorksVM recentWork)
        {
            bool isExists = await _appDbContext.RecentWorks.AnyAsync(x => x.Id == recentWork.Id);

            if (isExists)
            {
                ModelState.AddModelError("Recent Work", "Recent work alredy exists");
                return View();
            }
            if (!recentWork.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", "File type must be image");
                return View();
            }
            if (!recentWork.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image file must be size less than 200kb");
                return View();
            }

            string root = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img");
            string fileName = await recentWork.Photo.SaveAsync(root);

            RecentWork recentWork1 = new RecentWork()
            {
                CardTitle = recentWork.CardTitle,
                CardText = recentWork.CardText,
                ImagePath = fileName
            };

            await _appDbContext.AddAsync(recentWork1);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

            string imgPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets",
                "img", recentWork.ImagePath);
            if (System.IO.File.Exists(imgPath))
            {
                System.IO.File.Delete(imgPath);
            }

            _appDbContext.RecentWorks.Remove(recentWork);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
