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
        private const string CopyPathInsert = "../../../Products/products2.json";
        private const string CopyPathUpdate = "../../../Products/products3.json";

        public JsonProductFetchServiceEndToEndTest()
        {
            File.Copy(OriginalPath, CopyPathInsert);
            File.Copy(OriginalPath, CopyPathUpdate);
        }


        [Fact]
        public async Task TestInsertWithFileSave()
        {
            var jsonService = new JsonProductService(CopyPathInsert);
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
            var lines = await File.ReadAllLinesAsync(CopyPathInsert);
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


        [Fact]
        public async Task TestUpdate()
        {
            var jsonService = new JsonProductService(CopyPathUpdate);
            var product = await jsonService.Update(1, new JsonProductDto {Name = "Test"});

            product.Should().NotBeNull();
            product.Id.Should().Be(1);
            product.Name.Should().Be("Test");

            jsonService.Dispose();
            var lines = await File.ReadAllLinesAsync(CopyPathUpdate);
            var jsonProducts = new List<Product>();
            foreach (var line in lines)
            {
                if (line != string.Empty)
                {
                    jsonProducts.Add(JsonSerializer.Deserialize<Product>(line));
                }
            }

            jsonProducts.Should().NotBeNull().And.HaveCount(1);
            jsonProducts[0].Name.Should().Be("Test");
            jsonProducts[0].UpdatedAt.Should().NotBeNull();
        }

        public void Dispose()
        {
            if (File.Exists(CopyPathInsert))
            {
                File.Delete(CopyPathInsert);
            }

            if (File.Exists(CopyPathUpdate))
            {
                File.Delete(CopyPathUpdate);
            }
        }
    }
}