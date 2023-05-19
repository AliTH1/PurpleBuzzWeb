using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.Areas.Admin.ViewModels;
using PurpleBuzzWeb.DAL;
using PurpleBuzzWeb.Models;
using PurpleBuzzWeb.Utilities.Extensions;

namespace PurpleBuzzWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;
        private List<Category> _categories;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string _errorMessages;
        public ServicesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _categories = _context.Categories.ToList();
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(
                await _context.Services
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.Id)
                .Take(8)
                .Include(s => s.Category)
                .Include(s => s.ServiceImages)
                .ToListAsync()
                );
        }

        public IActionResult Create()
        {
            CreateServiceVM createServiceVM = new CreateServiceVM()
            {
                Categories = _categories
            };

            return View(createServiceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServiceVM createServiceVM)
        {
            createServiceVM.Categories = _categories;
            if (!ModelState.IsValid) { return View(createServiceVM); }
            if (!CheckPhoto(createServiceVM.Photos))
            {
                ModelState.AddModelError("Photos", _errorMessages);
                return View(createServiceVM);
            }

            string rootPath = Path.Combine(_webHostEnvironment.WebRootPath,
                "assets", "img");
            List<ServiceImage> serviceImages = await CreateAndGetServiceImages(
                createServiceVM.Photos, rootPath);

            Service service = new Service()
            {
                Name = createServiceVM.Name,
                CategoryId = createServiceVM.CategoryId,
                Description = createServiceVM.Description,
                Price = createServiceVM.Price,
                ServiceImages = serviceImages
            };



            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            Service? service = await _context.Services.FindAsync(id);

            if (service == null) return NotFound();
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img",
                service.ServiceImages.FirstOrDefault(c => c.ServiceId == id).Path);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }






        private async Task<List<ServiceImage>> CreateAndGetServiceImages(
            List<IFormFile> photos, string rootPath)
        {
            List<ServiceImage> serviceImages = new List<ServiceImage>();
            foreach (IFormFile photo in photos)
            {
                string fileName = await photo.SaveAsync(rootPath);
                ServiceImage serviceImage = new ServiceImage() { Path = fileName };
                if (!serviceImages.Any(i => i.IsActive))
                {
                    serviceImage.IsActive = true;
                }
                serviceImages.Add(serviceImage);
            }

            return serviceImages;
        }

        private bool CheckPhoto(List<IFormFile> photos)
        {
            foreach (IFormFile photo in photos)
            {
                if (!photo.CheckContentType("image/"))
                {
                    _errorMessages = "File type must be image";
                    return false;
                }
                if (!photo.CheckFileSize(200))
                {
                    _errorMessages = "Size must be less than 200kb";
                    return false;
                }
            }

            return true;
        }
    }
}
