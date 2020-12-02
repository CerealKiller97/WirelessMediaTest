using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models;

namespace Contracts.Products
{
    public interface IProductService
    {
        /// <summary>
        /// Fetches all products from storage
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Product>> Fetch(CancellationToken cancellationToken);
    }
}