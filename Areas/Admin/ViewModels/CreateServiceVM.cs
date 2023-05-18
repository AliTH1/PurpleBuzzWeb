using PurpleBuzzWeb.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurpleBuzzWeb.Areas.Admin.ViewModels
{
    public class CreateServiceVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
        [Required, NotMapped]
        public List<IFormFile> Photos { get; set; }
    }
}
