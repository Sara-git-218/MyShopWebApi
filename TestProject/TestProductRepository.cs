using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class TestProductRepository
    {
        [Fact]
        public async Task GetProducts_ReturnsAllProducts()
        {
            var products = new List<Product>
            {
                new Product { ProductName = "d1" },
                new Product { ProductName = "d2" }
            };

            var mockContext = new Mock<_326059268_ShopApiContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(products);

            var repo = new ProductRepository(mockContext.Object);
            var result = await repo.GetProducts();

            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task GetProducts_EmptyDb_ReturnsEmptyList()
        {
            var mockContext = new Mock<_326059268_ShopApiContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>());

            var repo = new ProductRepository(mockContext.Object);
            var result = await repo.GetProducts();

            Assert.Empty(result);
        }

    }
}
