using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Products;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Products
{
    public class DbProductService : IProductService
    {
        private readonly WirlessDbContext _context;

        public DbProductService(WirlessDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Product>> Fetch(CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}