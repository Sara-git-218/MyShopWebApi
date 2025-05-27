using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
   public class InegrationTestProductsRepository : IClassFixture<DatabaseFixture>
    {
        private readonly _326059268_ShopApiContext _dbContext;
        private readonly UserRepository _userRepository;
        public InegrationTestProductsRepository(DatabaseFixture fixture)
        {
            _dbContext = fixture.DbContext;
            _userRepository = new UserRepository(_dbContext);
        }
        [Fact]
        public async Task GetProducts_Should_Return_All_Products()
        {
            // Arrange
            _dbContext.Products.RemoveRange(_dbContext.Products);
            await _dbContext.SaveChangesAsync();

            var category = new Catgory { CatgoryName = "TestCategory" };
            await _dbContext.Catgories.AddAsync(category);
            await _dbContext.SaveChangesAsync(); // חייבים שיהיה לו Id

            var p1 = new Product {ProductName="test1",ProductDesdription="test1",Price=10, CatgoryId = category.CatgoryId };
            var p2 = new Product { ProductName = "test2", ProductDesdription = "test2", Price = 20 ,CatgoryId=category.CatgoryId};
            
            await _dbContext.Products.AddAsync(p1);
            await _dbContext.Products.AddAsync(p2);
            await _dbContext.SaveChangesAsync();

            // Act
            var products = await _dbContext.Products.ToListAsync();

            // Assert
            Assert.NotNull(products);
            Assert.True(products.Count >= 2);
            Assert.Contains(products, p => p.ProductName == "test1");
            Assert.Contains(products, p => p.ProductName == "test2");
        }
    }
}
