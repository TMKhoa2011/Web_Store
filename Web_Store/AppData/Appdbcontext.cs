using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_Store.Models;

namespace Web_Store.AppData
{
    public class Appdbcontext : IdentityDbContext<Web_Store.Data.ApplicationUser>
    {
        public Appdbcontext(DbContextOptions<Appdbcontext> options)
            : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Order_Product> OrderProducts { get; set; } = null!;
    }
}
