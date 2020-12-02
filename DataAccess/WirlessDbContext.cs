using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class WirlessDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public WirlessDbContext(DbContextOptions<WirlessDbContext> options): base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}