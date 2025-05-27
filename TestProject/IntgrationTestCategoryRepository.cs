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
    public class IntgrationTestCategoryRepository : IClassFixture<DatabaseFixture>
    {
        private readonly _326059268_ShopApiContext _dbContext;
        private readonly UserRepository _userRepository;
        public IntgrationTestCategoryRepository(DatabaseFixture fixture)
        {
            _dbContext = fixture.DbContext;
            _userRepository = new UserRepository(_dbContext);
        }
        [Fact]
        public async Task GetUsers_Should_Return_All_Users()
        {
            // Arrange
            _dbContext.Users.RemoveRange(_dbContext.Users);
            await _dbContext.SaveChangesAsync();

            var c1 = new Catgory { CatgoryName="test1"};
            var c2 = new Catgory { CatgoryName = "test2" };  
            await _dbContext.Catgories.AddAsync(c1);
            await _dbContext.Catgories.AddAsync(c2);
            await _dbContext.SaveChangesAsync();

            // Act
            var catgories = await _dbContext.Catgories.ToListAsync();

            // Assert
            Assert.NotNull(catgories);
            Assert.True(catgories.Count >= 2);
            Assert.Contains(catgories, c => c.CatgoryName == "test1");
            Assert.Contains(catgories, c => c.CatgoryName == "test2");
        }
    }
}
