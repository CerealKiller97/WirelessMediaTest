using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Services.Products;
using Xunit;

namespace Services.Test.Products
{
    public class JsonProductFetchServiceTest
    {
        [Fact]
        public async Task JsonFileNotFoundTest()
        {
            var jsonService = new JsonProductFetchService("./not-found.json");

            await Assert.ThrowsAsync<FileNotFoundException>(async () =>
            {
                await jsonService.Fetch(new CancellationToken());
            });
        }

        [Fact]
        public async Task JsonPathNullTest()
        {
            var jsonService = new JsonProductFetchService(null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await jsonService.Fetch(new CancellationToken());
            });
        }

        [Theory]
        [InlineData("")]
        public async Task JsonInvalidPathTest(string path)
        {
            var jsonService = new JsonProductFetchService(path);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await jsonService.Fetch(new CancellationToken());
            });
        }

        [Fact]
        public async Task JsonDataLoadedSuccessfully()
        {
            var jsonService = new JsonProductFetchService("../../../Products/products.json");

            var products = (await jsonService.Fetch(new CancellationToken())).ToList();

            products.Should().HaveCount(1);
            products[0].Should().NotBeNull();
            products[0].Name.Should().BeEquivalentTo("abc");
            products[0].Description.Should().BeEquivalentTo("Description 1");
        }
    }
}