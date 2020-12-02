using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Products;
using Models;

namespace Services.Products
{
    public class JsonProductService : IProductService
    {
        private readonly string _path;

        private static IEnumerable<Product> _products = null;
        public JsonProductService(string path)
        {
            _path = path;
        }
        public async Task<IEnumerable<Product>> Fetch(CancellationToken cancellationToken)
        {
            if (_products == null)
            {
                await Load(_path, cancellationToken);
            }

            return _products;
        }

        private static async Task Load(string path, CancellationToken cancellationToken)
        {
            await using var stream = File.OpenRead(path);
            
            _products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(stream, cancellationToken: cancellationToken);
        }
    }
}