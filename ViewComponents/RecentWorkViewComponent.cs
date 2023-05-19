using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.DAL;

namespace PurpleBuzzWeb.ViewComponents
{
    public class RecentWorkViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public RecentWorkViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(
                await _appDbContext.RecentWorks.ToListAsync()
                );
        }
    }
}
