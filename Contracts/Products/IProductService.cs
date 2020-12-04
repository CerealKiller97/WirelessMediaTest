using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models;
using Transfer;

namespace Contracts.Products
{
    public interface IProductService<TDto> where TDto : ProductDto
    {
        /// <summary>
        /// Fetches all products from storage
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Product>> Fetch(int page, int perPage, CancellationToken cancellationToken = default);

        public Task<Product> Insert(TDto dto);
    }
}