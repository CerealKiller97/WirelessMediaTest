using System.Collections.Generic;
using Contracts.Products;
using Models;

namespace Services.Products
{
    public class DbProductService : IProductService
    {
        public IEnumerable<Product> Fetch()
        {
            throw new System.NotImplementedException();
        }
    }
}