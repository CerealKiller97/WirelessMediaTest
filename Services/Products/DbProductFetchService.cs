using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Products;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Transfer;

namespace Services.Products
{
    public class DbProductService : IProductService<DbProductDto>
    {
        private readonly WirelessDbContext _context;

        public DbProductService(WirelessDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> Fetch(
            int page,
            int perPage,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product> FetchOne(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public Task<Product> Insert(DbProductDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> Update(int id, DbProductDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}