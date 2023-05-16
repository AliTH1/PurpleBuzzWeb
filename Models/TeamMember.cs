using System.ComponentModel.DataAnnotations;

namespace PurpleBuzzWeb.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string ImagePath { get; set; }
    }
}
