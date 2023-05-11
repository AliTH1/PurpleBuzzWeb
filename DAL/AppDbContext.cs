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

    }
}
