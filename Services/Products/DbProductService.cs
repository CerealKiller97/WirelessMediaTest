using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Products;
using Models;

namespace Services.Products
{
    public class DbProductService : IProductService
    {
        public async Task<IEnumerable<Product>> Fetch(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}