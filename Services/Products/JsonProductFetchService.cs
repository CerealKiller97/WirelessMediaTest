using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Products;
using Models;
using NodaTime;
using Transfer;

namespace Services.Products
{
    // Eventual Consistency Model 
    public class JsonProductService : IProductService<JsonProductDto>, IDisposable
    {
        private readonly string _path;
        private ConcurrentBag<Product> _products = new();
        private StreamWriter _writer;

        public JsonProductService(string path)
        {
            _path = path;
            if (_products.Count == 0)
            {
                Load();
            }

            _writer = File.AppendText(_path);
        }

        public async Task<IEnumerable<Product>> Fetch(
            int page,
            int perPage,
            CancellationToken cancellationToken = default
        )
        {
            return _products.Skip((page - 1) * perPage).Take(perPage);
        }

        public async Task<Product> Insert(JsonProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Manufacturer = dto.Manufacturer,
                Price = dto.Price,
                Vendor = dto.Vendor,
                Categories = dto.Categories,
                Description = dto.Description,
                CreatedAt = new Instant(),
                UpdatedAt = null,
            };

            _products.Add(product);

            var json = JsonSerializer.Serialize(product);
            await _writer.WriteLineAsync(json.ToCharArray());
            await _writer.FlushAsync();

            return product;
        }


        private void Load()
        {
            var lines = File.ReadAllLines(_path);
            foreach (var line in lines)
            {
                if (line != string.Empty)
                {
                    _products.Add(JsonSerializer.Deserialize<Product>(line));
                }
            }
        }

        public void Dispose()
        {
            _writer.Close();
        }
    }
}