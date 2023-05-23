using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.Models;
using PurpleBuzzWeb.Models.Auth;

namespace PurpleBuzzWeb.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
        public DbSet<WorkCategory> WorkCategories { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<RecentWork> RecentWorks { get; set; }

    }
}
