using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurpleBuzzWeb.Areas.Admin.ViewModels
{
    public class RecentWorksVM
    {
        public int Id { get; set; }
        public string CardTitle { get; set; }
        public string CardText { get; set; }
        [Required, NotMapped]
        public IFormFile Photo { get; set; }
    }
}
