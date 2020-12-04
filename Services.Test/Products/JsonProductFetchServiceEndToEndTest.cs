using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Models;
using Services.Products;
using Transfer;
using Xunit;

namespace Services.Test.Products
{
    public class JsonProductFetchServiceEndToEndTest : IDisposable
    {
        private const string OriginalPath = "../../../Products/products-e2e.json";
        private const string CopyPath = "../../../Products/products2.json";

        public JsonProductFetchServiceEndToEndTest()
        {
            File.Copy(OriginalPath, CopyPath);
        }


        [Fact]
        public async Task TestInsertWithFileSave()
        {
            var jsonService = new JsonProductService(CopyPath);
            var product = await jsonService.Insert(new JsonProductDto
            {
                Name = "Insert Test",
                Description = "Insert Test",
                Price = 2.0m,
                Categories = new List<Category> {new Category {Name = "Test Category"}}
            });
            var products = await jsonService.Fetch(1, 10);
            products.Should().NotBeNull()
                .And.HaveCount(2);

            product.Should().NotBeNull();
            product.Name.Should().Be("Insert Test");
            product.Description.Should().Be("Insert Test");
            product.Price.Should().Be(2.0m);
            product.Categories.Should().NotBeNull().And.HaveCount(1);
            jsonService.Dispose();
            var lines = await File.ReadAllLinesAsync(CopyPath);
            var jsonProducts = new List<Product>();
            foreach (var line in lines)
            {
                if (line != string.Empty)
                {
                    jsonProducts.Add(JsonSerializer.Deserialize<Product>(line));
                }
            }

            jsonProducts.Should().NotBeNull().And.HaveCount(2);
        }
        
        public void Dispose()
        {
            if (File.Exists(CopyPath))
            {
                File.Delete(CopyPath);
            }
        }
    }
}