using PurpleBuzzWeb.Models;

namespace PurpleBuzzWeb.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }
        public List<Service> Services { get; set; }
    }
}
