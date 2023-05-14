using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.DAL;
using PurpleBuzzWeb.Models;

namespace PurpleBuzzWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public WorkCategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ICollection<WorkCategory> workCategories = await _context.WorkCategories.ToListAsync();
            return View(workCategories);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkCategory workCategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExists = await _context.WorkCategories.AnyAsync(c =>
            c.Name.ToLower().Trim() == workCategory.Name.ToLower().Trim());

            if (isExists)
            {
                ModelState.AddModelError("Name", "Category name already exists");
                return View();
            }
            await _context.WorkCategories.AddAsync(workCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id) 
        {
            WorkCategory? workCategory = _context.WorkCategories.Find(id);
            if (workCategory == null)
            {
                return NotFound();
            }


            return View(workCategory);
        }

        [HttpPost]
        public IActionResult Update(WorkCategory workCategory)
        {
            WorkCategory? editedWorkCategory = _context.WorkCategories.Find(workCategory.Id);

            if (editedWorkCategory == null)
            {
                return NotFound();
            }

            editedWorkCategory.Name = workCategory.Name;
            _context.WorkCategories.Update(editedWorkCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            WorkCategory? workCategory = _context.WorkCategories.Find(id);
            _context.WorkCategories.Remove(workCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
