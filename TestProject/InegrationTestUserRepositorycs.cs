using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
   public  class InegrationTestUserRepository : IClassFixture<DatabaseFixture>
    {
        private readonly _326059268_ShopApiContext _dbContext;
        private readonly UserRepository _userRepository;
        public InegrationTestUserRepository(DatabaseFixture fixture)
        {
            _dbContext = fixture.DbContext;
            _userRepository = new UserRepository(_dbContext);
        }

        [Fact]
        public async Task Register_Should_Add_User()
        {
            // Arrange
            var user = new User
            {
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Password = "1234"
            };

            // Act
            //var result = await _userRepository.Register(user);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            // Assert
            //Assert.NotNull(result);
            //Assert.Equal("testuser", result.UserName);
            var fromDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName.Trim() == "testuser");
            Assert.NotNull(fromDb);
            Assert.Equal("Test", fromDb.FirstName.Trim());
        }
    }
}
