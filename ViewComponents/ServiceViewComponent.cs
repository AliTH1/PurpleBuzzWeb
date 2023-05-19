using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.DAL;

namespace PurpleBuzzWeb.ViewComponents
{
    public class ServiceViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ServiceViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(
                await _context.Services
                .OrderByDescending(s => s.Id)
                .Where(c => !c.IsDeleted)
                .Take(8)
                .Include(c => c.ServiceImages)
                .ToListAsync()
    );


        }

    }
}
