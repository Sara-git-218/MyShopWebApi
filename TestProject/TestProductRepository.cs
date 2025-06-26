//uncomment and maintain the tests.
//using Entities;
//using Moq;
//using Moq.EntityFrameworkCore;
//using Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TestProject
//{
//    public class TestProductRepository
//    {
//        [Fact]
//        public async Task GetProducts_ReturnsAllProducts()
//        {
//            var products = new List<Product>
//            {
//                new Product { ProductName = "d1" },
//                new Product { ProductName = "d2" }
//            };

//            var mockContext = new Mock<_326059268_ShopApiContext>();
//            mockContext.Setup(c => c.Products).ReturnsDbSet(products);

//            var repo = new ProductRepository(mockContext.Object);
//            var result = await repo.GetProducts();

//            Assert.Equal(2, result.Count);
//        }
//        [Fact]
//        public async Task GetProducts_EmptyDb_ReturnsEmptyList()
//        {
//            var mockContext = new Mock<_326059268_ShopApiContext>();
//            mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>());

//            var repo = new ProductRepository(mockContext.Object);
//            var result = await repo.GetProducts();

//            Assert.Empty(result);
//        }

//    }
//}

//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Entities;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using Moq.EntityFrameworkCore;
//using Repositories;
//using Xunit;

//public class ProductRepositoryTests
//{
//    [Fact]
//    public async Task GetProducts_FiltersAndOrdersCorrectly()
//    {
//        // Arrange
//        var category1 = new Catgory { CatgoryId = 1, CatgoryName = "A" };
//        var category2 = new Catgory { CatgoryId = 2, CatgoryName = "B" };

//        var products = new List<Product>
//        {
//            new Product { ProductId = 1, ProductName = "P1", ProductDesdription = "desc1", Price = 50, CatgoryId = 1, Catgory = category1 },
//            new Product { ProductId = 2, ProductName = "P2", ProductDesdription = "desc2", Price = 100, CatgoryId = 2, Catgory = category2 },
//            new Product { ProductId = 3, ProductName = "P3", ProductDesdription = "something else", Price = 150, CatgoryId = 2, Catgory = category2 }
//        };

//        var mockContext = new Mock<_326059268_ShopApiContext>();
//        mockContext.Setup(x => x.Products).ReturnsDbSet(products);

//        var repo = new ProductRepository(mockContext.Object);

//        // Act
//        var result = await repo.GetProducts("desc", 50, 120, new int?[] { 1, 2 });

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal(2, result.Count); // P1 and P2
//        Assert.Contains(result, p => p.ProductName == "P1");
//        Assert.Contains(result, p => p.ProductName == "P2");
//        Assert.DoesNotContain(result, p => p.ProductName == "P3");
//        Assert.True(result[0].Price <= result[1].Price); // Sorted by price
//    }

//    [Fact]
//    public async Task GetProducts_NoFilters_ReturnsAllProductsSorted()
//    {
//        // Arrange
//        var category1 = new Catgory { CatgoryId = 1, CatgoryName = "A" };
//        var category2 = new Catgory { CatgoryId = 2, CatgoryName = "B" };

//        var products = new List<Product>
//        {
//            new Product { ProductId = 1, ProductName = "P1", ProductDesdription = "desc1", Price = 200, CatgoryId = 1, Catgory = category1 },
//            new Product { ProductId = 2, ProductName = "P2", ProductDesdription = "desc2", Price = 100, CatgoryId = 2, Catgory = category2 },
//        };

//        var mockContext = new Mock<_326059268_ShopApiContext>();
//        mockContext.Setup(x => x.Products).ReturnsDbSet(products);

//        var repo = new ProductRepository(mockContext.Object);

//        // Act
//        var result = await repo.GetProducts(null, null, null, new int?[0]);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal(2, result.Count);
//        Assert.Equal("P2", result.First().ProductName); // Sorted by price
//        Assert.Equal("P1", result.Last().ProductName);
//    }
//}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;

public class ProductRepositoryTests
{
    private List<Product> GetTestProducts()
    {
        var category1 = new Catgory { CatgoryId = 1, CatgoryName = "A" };
        var category2 = new Catgory { CatgoryId = 2, CatgoryName = "B" };

        return new List<Product>
        {
            new Product { ProductId = 1, ProductName = "P1", ProductDesdription = "foo bar", Price = 10, CatgoryId = 1, Catgory = category1 },
            new Product { ProductId = 2, ProductName = "P2", ProductDesdription = "bar baz", Price = 20, CatgoryId = 2, Catgory = category2 },
            new Product { ProductId = 3, ProductName = "P3", ProductDesdription = "lorem ipsum", Price = 30, CatgoryId = 1, Catgory = category1 },
            new Product { ProductId = 4, ProductName = "P4", ProductDesdription = "test product", Price = 40, CatgoryId = 2, Catgory = category2 }
        };
    }

    private ProductRepository GetRepository()
    {
        var mockContext = new Mock<_326059268_ShopApiContext>();
        mockContext.Setup(x => x.Products).ReturnsDbSet(GetTestProducts());
        return new ProductRepository(mockContext.Object);
    }

    [Fact]
    public async Task GetProducts_FilterByDesc_ReturnsCorrectProducts()
    {
        var repo = GetRepository();
        var result = await repo.GetProducts("foo", null, null, new int?[0]);
        Assert.Single(result);
        Assert.Equal("P1", result.First().ProductName);
    }

    [Fact]
    public async Task GetProducts_FilterByMinPrice()
    {
        var repo = GetRepository();
        var result = await repo.GetProducts(null, 30, null, new int?[0]);
        Assert.Equal(2, result.Count);
        Assert.All(result, p => Assert.True(p.Price >= 30));
    }

    [Fact]
    public async Task GetProducts_FilterByMaxPrice()
    {
        var repo = GetRepository();
        var result = await repo.GetProducts(null, null, 20, new int?[0]);
        Assert.Equal(2, result.Count);
        Assert.All(result, p => Assert.True(p.Price <= 20));
    }

    [Fact]
    public async Task GetProducts_FilterByCategoryIds()
    {
        var repo = GetRepository();
        var result = await repo.GetProducts(null, null, null, new int?[] { 2 });
        Assert.Equal(2, result.Count);
        Assert.All(result, p => Assert.Equal(2, p.CatgoryId));
    }

    [Fact]
    public async Task GetProducts_FilterByAllParameters()
    {
        var repo = GetRepository();
        var result = await repo.GetProducts("bar", 10, 20, new int?[] { 2 });
        Assert.Single(result);
        Assert.Equal("P2", result.First().ProductName);
    }

    [Fact]
    public async Task GetProducts_EmptyCategoryIds_ReturnsAll()
    {
        var repo = GetRepository();
        var result = await repo.GetProducts(null, null, null, new int?[0]);
        Assert.Equal(4, result.Count);
    }
}
