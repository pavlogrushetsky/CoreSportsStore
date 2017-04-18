using Microsoft.EntityFrameworkCore;

namespace CoreSportsStore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {   
        }

        public DbSet<Product> Products { get; set; }
    }
}