using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Models;
using Services.Products;
using Transfer;
using Xunit;

namespace Services.Test.Products
{
    public class JsonProductFetchServiceIntegrationTest
    {
        private const string OriginalPath = "../../../Products/products.json";

        [Fact]
        public async Task JsonDataLoadedSuccessfully()
        {
            using var jsonService = new JsonProductService(OriginalPath);

            // ACT
            var products = (await jsonService.Fetch(1, 10)).ToList();

            // ASSERT
            products.Should().HaveCount(1);

            var product = products[0];
            product.Should().NotBeNull();
            product.Id.Should().Be(1);
            product.Name.Should().Be("abc");
            product.Description.Should().Be("Description 1");
            product.Price.Should().Be(123);
            product.Categories.Should().NotBeNull().And.HaveCount(2);

            // Categories
            var categories = product.Categories;
            categories[0].Id.Should().Be(1);
            categories[0].Name.Should().Be("test category 1");
            categories[1].Id.Should().Be(2);
            categories[1].Name.Should().Be("test category 2");

            var vendor = product.Vendor;
            vendor.Should().NotBeNull();
            vendor.Id.Should().Be(1);
            vendor.Name.Should().Be("Test Vendor");

            var manufacturer = product.Manufacturer;
            manufacturer.Should().NotBeNull();
            manufacturer.Id.Should().Be(1);
            manufacturer.Name.Should().Be("Test manufacturer");
        }
        
        [Fact]
        public async Task JsonPathNullTest()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                using var jsonService = new JsonProductService(null);
                await jsonService.Fetch(1, 10);
            });
        }

        [Theory]
        [InlineData("")]
        public async Task JsonInvalidPathTest(string path)
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var jsonService = new JsonProductService(path);
                await jsonService.Fetch(1, 10);
            });
        }
    }
}