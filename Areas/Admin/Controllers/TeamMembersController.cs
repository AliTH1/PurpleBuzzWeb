using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.Areas.ViewModels;
using PurpleBuzzWeb.DAL;
using PurpleBuzzWeb.Models;
using PurpleBuzzWeb.Utilities.Extensions;

namespace PurpleBuzzWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMembersController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeamMembersController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            ICollection<TeamMember> teamMembers = await _appDbContext.TeamMembers.ToListAsync();
            return View(teamMembers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamMembersVM teamMember)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!teamMember.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", "File type must be image");
                return View();
            }
            if (!teamMember.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image file must be size less than 200kb");
                return View();
            }


            string root = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img");
            string fileName = await teamMember.Photo.SaveAsync(root);

            TeamMember teamMember1 = new TeamMember()
            {
                FullName = teamMember.FullName,
                Profession = teamMember.Profession,
                ImagePath = fileName
            };

            await _appDbContext.AddAsync(teamMember1);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            TeamMember? teamMember = await _appDbContext.TeamMembers.FindAsync(id);

            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, string fullName, string profession,
            string imagePath)
        {
            TeamMember? teamMember = await _appDbContext.TeamMembers.FindAsync(id);

            if (teamMember == null)
            {
                return NotFound();
            }

            teamMember.FullName = fullName;
            teamMember.Profession = profession;
            teamMember.ImagePath = imagePath;

            _appDbContext.TeamMembers.Update(teamMember);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            TeamMember? teamMember = await _appDbContext.TeamMembers.FindAsync(id);

            if(teamMember == null) return NotFound();
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img",
                teamMember.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _appDbContext.TeamMembers.Remove(teamMember);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
