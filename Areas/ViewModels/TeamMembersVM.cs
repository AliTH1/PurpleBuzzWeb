using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurpleBuzzWeb.Areas.ViewModels
{
    public class TeamMembersVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FullName { get; set; }
        public string Profession { get; set; }
        [Required, NotMapped]
        public IFormFile Photo { get; set; }
    }
}
