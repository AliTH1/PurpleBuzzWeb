using Microsoft.AspNetCore.Mvc;

namespace PurpleBuzzWeb.Controllers
{
    public class PricingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
