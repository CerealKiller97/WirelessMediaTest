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
        private readonly ConcurrentBag<Product> _products = new();
        private static readonly object _lockObject = new();
        private StreamWriter _writer;
        private int _id;

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

        public async Task<Product> FetchOne(int id)
        {
            return _products.SingleOrDefault(p => p.Id == id);
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
                CreatedAt = SystemClock.Instance.GetCurrentInstant(),
                UpdatedAt = null,
            };

            lock (_lockObject)
            {
                product.Id = ++_id;
                _products.Add(product);
            }

            var json = JsonSerializer.Serialize(product);
            await _writer.WriteLineAsync(json.ToCharArray());
            await _writer.FlushAsync();

            return product;
        }

        public async Task<Product> Update(int id, JsonProductDto dto)
        {
            var product = _products.Single(p => p.Id == id);

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Manufacturer = dto.Manufacturer;
            product.Vendor = dto.Vendor;
            product.UpdatedAt = SystemClock.Instance.GetCurrentInstant();

            lock (_lockObject)
            {
                _writer.Dispose();
                var handler = File.Open(_path, FileMode.Open, FileAccess.ReadWrite);
                handler.SetLength(0);
                handler.Flush();
                handler.Dispose();
                _writer = File.AppendText(_path);
                foreach (var prod in _products)
                {
                    var json = JsonSerializer.Serialize(product);
                    _writer.WriteLine(json.ToCharArray());
                }

                _writer.Flush();
            }

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

            _id = _products.LastOrDefault()?.Id ?? 1;
        }

        public void Dispose()
        {
            _writer.Close();
        }
    }
}