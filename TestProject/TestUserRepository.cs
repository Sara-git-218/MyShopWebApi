
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;

namespace TestProject
{
    public class TestUserRepository
    {
        // --- LOGIN ---

        [Fact]
        public async Task Login_UserExists_ReturnsUser()
        {
            var user = new User { UserName = "user1", Password = "pass" };
            var users = new List<User> { user };

            var mockContext = new Mock<_326059268_ShopApiContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(users);

            var repo = new UserRepository(mockContext.Object);
            var result = await repo.Login("user1");

            Assert.NotNull(result);
            Assert.Equal("user1", result.UserName);
        }

        [Fact]
        public async Task Login_UserDoesNotExist_ReturnsNull()
        {
            var mockContext = new Mock<_326059268_ShopApiContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

            var repo = new UserRepository(mockContext.Object);
            var result = await repo.Login("unknown");

            Assert.Null(result);
        }

        // --- GET USERS ---

        [Fact]
        public async Task GetUsers_ReturnsAllUsers()
        {
            var users = new List<User>
            {
                new User { UserName = "u1" },
                new User { UserName = "u2" }
            };

            var mockContext = new Mock<_326059268_ShopApiContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(users);

            var repo = new UserRepository(mockContext.Object);
            var result = await repo.GetUsers();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetUsers_EmptyDb_ReturnsEmptyList()
        {
            var mockContext = new Mock<_326059268_ShopApiContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

            var repo = new UserRepository(mockContext.Object);
            var result = await repo.GetUsers();

            Assert.Empty(result);
        }

        // --- REGISTER ---

        [Fact]
        public async Task Register_ValidUser_AddsAndReturnsUser()
        {
            var user = new User { UserName = "newuser" };
            var users = new List<User>();

            var mockContext = new Mock<_326059268_ShopApiContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(users);
            mockContext.Setup(c => c.Users.AddAsync(It.IsAny<User>(), default))
                       .Callback<User, CancellationToken>((u, _) => users.Add(u))
                       .ReturnsAsync((User u, CancellationToken _) => null);
            mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            var repo = new UserRepository(mockContext.Object);
            var result = await repo.Register(user);

            Assert.Single(users);
            Assert.Equal("newuser", result.UserName);
        }

        [Fact]
        public async Task Register_NullUser_ThrowsException()
        {
            var mockContext = new Mock<_326059268_ShopApiContext>();
            var repo = new UserRepository(mockContext.Object);

            await Assert.ThrowsAsync<System.ArgumentNullException>(() => repo.Register(null));
        }

        // --- UPDATE ---

        [Fact]
        public async Task Update_ValidUser_UpdatesAndReturnsUser()
        {
            var user = new User { Id = 1, UserName = "updated" };

            var mockContext = new Mock<_326059268_ShopApiContext>();
            mockContext.Setup(c => c.Users.Update(user));
            mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            var repo = new UserRepository(mockContext.Object);
            var result = await repo.UpDate(user, 1);

            Assert.Equal("updated", result.UserName);
            mockContext.Verify(c => c.Users.Update(user), Times.Once);
        }

        [Fact]
        public async Task Update_NullUser_ThrowsException()
        {
            var mockContext = new Mock<_326059268_ShopApiContext>();
            var repo = new UserRepository(mockContext.Object);

            await Assert.ThrowsAsync<System.ArgumentNullException>(() => repo.UpDate(null, 1));
        }

        [Fact]
        public async Task Update_IdMismatch_StillUpdatesUser()
        {
            var user = new User { Id = 5, UserName = "mismatch" };

            var mockContext = new Mock<_326059268_ShopApiContext>();
            mockContext.Setup(c => c.Users.Update(user));
            mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            var repo = new UserRepository(mockContext.Object);
            var result = await repo.UpDate(user, 99); // id לא תואם

            Assert.Equal("mismatch", result.UserName);
        }
    }
}

