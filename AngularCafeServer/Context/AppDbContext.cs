using AngularCafeServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngularCafeServer.Context
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Brew> Brews { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
    }
}
