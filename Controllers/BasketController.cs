using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PurpleBuzzWeb.DAL;
using PurpleBuzzWeb.ViewModel;

namespace PurpleBuzzWeb.Controllers
{
    public class BasketController : Controller
    {
        private const string COOKIES_BASKET = "basketVM";
        private readonly AppDbContext _context;

        public BasketController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<BasketVM> basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>
                (Request.Cookies[COOKIES_BASKET]);

            List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();

            foreach (BasketVM item in basketVMs)
            {
                BasketItemVM basketItemVM = _context.Services
                    .Where(s => s.Id == item.ServiceId && !s.IsDeleted)
                    .Include(s => s.Category)
                    .Include(s => s.ServiceImages)
                    .Select(s => new BasketItemVM
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CategoryName = s.Category.Name,
                        IsDeleted = s.IsDeleted,
                        ServiceCount = item.Count,
                        Price = s.Price,
                        ImagePath = s.ServiceImages.FirstOrDefault(s => s.IsActive).Path
                    }).FirstOrDefault();
                basketItemVMs.Add(basketItemVM);
            }
            return View(basketItemVMs);
        }


        public void AddBasket(int id)
        {
            List<BasketVM> basketVMs;

            if (Request.Cookies[COOKIES_BASKET] != null)
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>
                    (Request.Cookies[COOKIES_BASKET]);
            }
            else
            {
                basketVMs = new List<BasketVM>();
            }

            BasketVM cookiesBasket = basketVMs.Where(s => s.ServiceId == id).FirstOrDefault();

            if (cookiesBasket != null)
            {
                cookiesBasket.Count++;
            }
            else
            {
                BasketVM basketVM = new BasketVM()
                {
                    ServiceId = id,
                    Count = 1
                };
                basketVMs.Add(basketVM);

            }
            Response.Cookies.Append(COOKIES_BASKET, JsonConvert.SerializeObject(basketVMs));
        }


        //public async Task<IActionResult> IncreaseItemInBasket(int id, BasketItemVM basketItemVM)
        //{
        //    BasketItemVM basketItem = await 

        //    return RedirectToAction(nameof(Index));

        //}
    }
}
