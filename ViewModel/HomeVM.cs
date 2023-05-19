using PurpleBuzzWeb.Models;

namespace PurpleBuzzWeb.ViewModels
{
    public class HomeVM
    {
        public List<Category> Categories { get; set; }
        public List<RecentWork> RecentWorks { get; set; }
    }
}
