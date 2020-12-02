using System.Collections.Generic;
using Models;

namespace Contracts.Products
{
    public interface IProductService
    {
        /// <summary>
        /// Fetches all products from storage
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> Fetch();
    }
}