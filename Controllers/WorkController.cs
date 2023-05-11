using Microsoft.AspNetCore.Mvc;

namespace PurpleBuzzWeb.Controllers
{
    public class WorkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
