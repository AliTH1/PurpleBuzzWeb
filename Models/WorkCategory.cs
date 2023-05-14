using System.ComponentModel.DataAnnotations;

namespace PurpleBuzzWeb.Models
{
    public class WorkCategory
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
