using Microsoft.AspNetCore.Mvc;

namespace PurpleBuzzWeb.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
