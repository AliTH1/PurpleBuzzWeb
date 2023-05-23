using Microsoft.AspNetCore.Identity;

namespace PurpleBuzzWeb.Models.Auth
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActivated { get; set; }
    }
}
