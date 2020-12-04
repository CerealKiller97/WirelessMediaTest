using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Extensions
{
    public static class DatabaseSeeder
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(new List<Product>
            {
                new Product
                {
                    Id = 1
                }
            });
        }
    }
}