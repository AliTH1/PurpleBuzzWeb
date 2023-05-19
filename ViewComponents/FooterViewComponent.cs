using Microsoft.AspNetCore.Mvc;

namespace PurpleBuzzWeb.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
