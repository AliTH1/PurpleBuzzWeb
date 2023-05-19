using Microsoft.AspNetCore.Mvc;

namespace PurpleBuzzWeb.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
