using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.DAL;
using PurpleBuzzWeb.Models;

namespace PurpleBuzzWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMembersController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public TeamMembersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
        public async Task<IActionResult> Create(TeamMember teamMember)
        {
            TeamMember member = new TeamMember();

            if (!ModelState.IsValid)
            {
                return View();
            }
            member.FullName = teamMember.FullName;
            member.Profession = teamMember.Profession;
            member.ImagePath = teamMember.ImagePath;

            await _appDbContext.TeamMembers.AddAsync(member);
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

            if(teamMember == null)
            {
                return NotFound();
            }

            _appDbContext.TeamMembers.Remove(teamMember);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
