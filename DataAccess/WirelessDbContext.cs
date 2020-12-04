using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class WirelessDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public WirelessDbContext(DbContextOptions<WirelessDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}