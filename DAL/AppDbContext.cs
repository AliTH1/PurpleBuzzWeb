using Microsoft.EntityFrameworkCore;
using PurpleBuzzWeb.Models;

namespace PurpleBuzzWeb.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
        public DbSet<WorkCategory> WorkCategories { get; set; }

    }
}
