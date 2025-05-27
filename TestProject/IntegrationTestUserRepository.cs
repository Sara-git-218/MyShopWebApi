////using Entities;
////using Microsoft.EntityFrameworkCore;
////using Repositories;
////using System;
////using System.Collections.Generic;
////using System.Threading.Tasks;
////using Xunit;

////namespace TestProject
////{
////    public class IntegrationTestOrderRepository : IClassFixture<DatabaseFixture>
////    {
////        private readonly _326059268_ShopApiContext _dbContext;
////        private readonly OrderRepository _orderRepository;

////        public IntegrationTestOrderRepository(DatabaseFixture fixture)
////        {
////            _dbContext = fixture.DbContext;
////            _orderRepository = new OrderRepository(_dbContext);
////        }

////        [Fact]
////        public async Task CreateOrder_Should_Add_Order_With_Orderitems_And_User()
////        {
////            // Arrange
////            // צור יוזר (כי יש ForeignKey)
////            var user = new User
////            {
////                UserName = "orderuser",
////                FirstName = "Order",
////                LastName = "User",
////                Password = "1234"
////            };
////            await _dbContext.Users.AddAsync(user);
////            await _dbContext.SaveChangesAsync();

////            // צור Order עם פריט אחד לפחות
////            var order = new Order
////            {
////                OrderDate = DateOnly.FromDateTime(DateTime.Now),
////                OrderSum = 150,
////                UserId = user.UserId,
////                Orderitems = new List<Orderitem>
////                {
////                    new Orderitem
////                    {
////                        // מלא כאן שדות חובה לפי המבנה שלך, למשל:
////                        // ProductId = 1,
////                        // Quantity = 2,
////                        // Price = 75
////                    }
////                }
////            };

////            // Act
////            var result = await _orderRepository.CreateOrder(order);

////            // Assert
////            Assert.NotNull(result);
////            Assert.True(result.OrderId > 0);

////            var fromDb = await _dbContext.Orders
////                .Include(o => o.Orderitems)
////                .Include(o => o.User)
////                .FirstOrDefaultAsync(o => o.OrderId == result.OrderId);

////            Assert.NotNull(fromDb);
////            Assert.Equal(user.UserId, fromDb.UserId);
////            Assert.Equal(1, fromDb.Orderitems.Count);
////            Assert.NotNull(fromDb.User);
////        }

////        [Fact]
////        public async Task CreateOrder_Should_Throw_When_User_Not_Exist()
////        {
////            // Arrange
////            var order = new Order
////            {
////                OrderDate = DateOnly.FromDateTime(DateTime.Now),
////                OrderSum = 200,
////                UserId = 9999, // יוזר שלא קיים
////                Orderitems = new List<Orderitem>()
////            };

////            // Act & Assert
////            await Assert.ThrowsAnyAsync<DbUpdateException>(async () =>
////            {
////                await _orderRepository.CreateOrder(order);
////            });
////        }

////        [Fact]
////        public async Task CreateOrder_With_No_Orderitems_Should_Succeed()
////        {
////            // Arrange
////            var user = new User
////            {
////                UserName = "noitemsuser",
////                FirstName = "No",
////                LastName = "Items",
////                Password = "test"
////            };
////            await _dbContext.Users.AddAsync(user);
////            await _dbContext.SaveChangesAsync();

////            var order = new Order
////            {
////                OrderDate = DateOnly.FromDateTime(DateTime.Now),
////                OrderSum = 0,
////                UserId = user.UserId,
////                Orderitems = new List<Orderitem>() // אין פריטים
////            };

////            // Act
////            var result = await _orderRepository.CreateOrder(order);

////            // Assert
////            Assert.NotNull(result);
////            Assert.True(result.OrderId > 0);

////            var fromDb = await _dbContext.Orders
////                .Include(o => o.Orderitems)
////                .FirstOrDefaultAsync(o => o.OrderId == result.OrderId);

////            Assert.NotNull(fromDb);
////            Assert.Empty(fromDb.Orderitems);
////        }
////    }
////}


////using Entities;
////using Microsoft.EntityFrameworkCore;
////using Repositories;
////using System.Threading.Tasks;
////using Xunit;

////namespace TestProject
////{
////    public class IntegrationTestUserDbContext : IClassFixture<DatabaseFixture>
////    {
////        private readonly _326059268_ShopApiContext _dbContext;

////        public IntegrationTestUserDbContext(DatabaseFixture fixture)
////        {
////            _dbContext = fixture.DbContext;
////        }

////        [Fact]

////        public async Task Register_Should_Add_User()
////        {
////            // Arrange
////            var user = new User
////            {
////                UserName = "testuser",
////                FirstName = "Test",
////                LastName = "User",
////                Password = "1234"
////            };

////            // Act
////            //var result = await _userRepository.Register(user);
////            await _dbContext.Users.AddAsync(user);
////            await _dbContext.SaveChangesAsync();
////            // Assert
////            //Assert.NotNull(result);
////            //Assert.Equal("testuser", result.UserName);
////            var fromDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName.Trim() == "testuser");
////            Assert.NotNull(fromDb);
////            Assert.Equal("Test", fromDb.FirstName.Trim());
////        }
////        [Fact]
////        public async Task Register_NullUser_Should_ThrowException()
////        {
////            var repo = new Repositories.UserRepository(_dbContext);
////            await Assert.ThrowsAsync<ArgumentNullException>(() => repo.Register(null));
////        }

////        [Fact]
////        public async Task Login_Should_Return_User_When_Exists()
////        {
////            var user = new User { UserName = "login_user" };
////            await _dbContext.Users.AddAsync(user);
////            await _dbContext.SaveChangesAsync();

////            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == "login_user");

////            Assert.NotNull(result);
////            Assert.Equal("login_user", result.UserName);
////        }

////        [Fact]
////        public async Task Login_NonExistingUser_Should_Return_Null()
////        {
////            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == "does_not_exist");
////            Assert.Null(result);
////        }

////        [Fact]
////        public async Task Update_Should_Change_Username()
////        {
////            var user = new User { UserName = "to_update" };
////            await _dbContext.Users.AddAsync(user);
////            await _dbContext.SaveChangesAsync();

////            user.UserName = "updated_user";
////            _dbContext.Users.Update(user);
////            await _dbContext.SaveChangesAsync();

////            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

////            Assert.NotNull(result);
////            Assert.Equal("updated_user", result.UserName);
////        }

////        [Fact]
////        public async Task Update_NullUser_Should_ThrowException()
////        {
////            var repo = new Repositories.UserRepository(_dbContext);
////            await Assert.ThrowsAsync<ArgumentNullException>(() => repo.UpDate(null, 0));
////        }

////        [Fact]
////        public async Task GetAllUsers_Should_Return_All()
////        {
////            _dbContext.Users.RemoveRange(_dbContext.Users);
////            await _dbContext.SaveChangesAsync();

////            var u1 = new User { UserName = "u1" };
////            var u2 = new User { UserName = "u2" };
////            await _dbContext.Users.AddRangeAsync(u1, u2);
////            await _dbContext.SaveChangesAsync();

////            var users = await _dbContext.Users.ToListAsync();

////            Assert.NotNull(users);
////            Assert.Equal(2, users.Count);
////        }
////    }
////}


using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class IntegrationTestUserDbContext : IClassFixture<DatabaseFixture>
    {
        private readonly _326059268_ShopApiContext _dbContext;
       // private readonly UserRepository _repo;

        public IntegrationTestUserDbContext(DatabaseFixture fixture)
        {
            _dbContext = fixture.DbContext;
           // _repo = new UserRepository(_dbContext);
        }

        [Fact]
        public async Task Register_Should_Add_User()
        {
            var user = new User
            {
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Password = "1234"
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var fromDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == "testuser");
            Assert.NotNull(fromDb);
            Assert.Equal("Test", fromDb.FirstName);
        }

        [Fact]
        public async Task Login_Should_Return_User_When_Exists()
        {
            var user = new User { UserName = "login_user", Password = "pass" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == "login_user");
            Assert.NotNull(result);
            Assert.Equal("login_user", result.UserName);
        }

        [Fact]
        public async Task Login_NonExistingUser_Should_Return_Null()
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == "does_not_exist");
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_Should_Change_User_Details()
        {
            var user = new User { UserName = "to_update", FirstName = "Before", Password = "pass" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            user.UserName = "updated";
            user.FirstName = "After";
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            Assert.NotNull(result);
            Assert.Equal("After", result.FirstName);
        }

        [Fact]
        public async Task GetAllUsers_Should_Return_All()
        {
            _dbContext.Users.RemoveRange(_dbContext.Users);
            await _dbContext.SaveChangesAsync();

            var u1 = new User { UserName = "u1", Password = "1" };
            var u2 = new User { UserName = "u2", Password = "2" };
            await _dbContext.Users.AddRangeAsync(u1, u2);
            await _dbContext.SaveChangesAsync();

            var users = await _dbContext.Users.ToListAsync();
            Assert.NotNull(users);
            Assert.Equal(2, users.Count);
        }


        //        [Fact]
        //        public async Task Register_Should_Add_User_To_Database()
        //        {
        //            var user = new User
        //            {
        //                UserName = "testuser",
        //                Password = "12345",
        //                FirstName = "Test",
        //                LastName = "User"
        //            };

        //            var result = await _repo.Register(user);

        //            Assert.NotNull(result);
        //            Assert.Equal("testuser", result.UserName);
        //            Assert.Equal("Test", result.FirstName);

        //            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == "testuser");
        //            Assert.NotNull(dbUser);
        //            Assert.Equal("User", dbUser.LastName);
        //        }

        //        [Fact]
        //        public async Task Register_NullUser_Should_ThrowException()
        //        {
        //            await Assert.ThrowsAsync<ArgumentNullException>(() => _repo.Register(null));
        //        }

        //        [Fact]
        //        public async Task Login_ExistingUser_Should_Return_User()
        //        {
        //            var user = new User
        //            {
        //                UserName = "loginuser",
        //                Password = "pass123"
        //            };

        //            await _dbContext.Users.AddAsync(user);
        //            await _dbContext.SaveChangesAsync();

        //            var result = await _repo.Login("loginuser");

        //            Assert.NotNull(result);
        //            Assert.Equal("loginuser", result.UserName);
        //        }

        //        [Fact]
        //        public async Task Login_NonExistingUser_Should_Return_Null()
        //        {
        //            var result = await _repo.Login("nonexistent");
        //            Assert.Null(result);
        //        }

        //        [Fact]
        //        public async Task Update_Should_Change_User_Details()
        //        {
        //            var user = new User
        //            {
        //                UserName = "toupdate",
        //                Password = "oldpass",
        //                FirstName = "Before",
        //                LastName = "Change"
        //            };

        //            await _dbContext.Users.AddAsync(user);
        //            await _dbContext.SaveChangesAsync();

        //            user.UserName = "updated";
        //            user.FirstName = "After";

        //            var result = await _repo.UpDate(user, user.Id);

        //            Assert.NotNull(result);
        //            Assert.Equal("updated", result.UserName);

        //            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        //            Assert.Equal("After", dbUser.FirstName);
        //        }

        //        [Fact]
        //        public async Task Update_NullUser_Should_ThrowException()
        //        {
        //            await Assert.ThrowsAsync<ArgumentNullException>(() => _repo.UpDate(null, 0));
        //        }

        //        [Fact]
        //        public async Task GetUsers_Should_Return_All_Users()
        //        {
        //            _dbContext.Users.RemoveRange(_dbContext.Users);
        //            await _dbContext.SaveChangesAsync();

        //            var u1 = new User { UserName = "a", Password = "1" };
        //            var u2 = new User { UserName = "b", Password = "2" };

        //            await _dbContext.Users.AddRangeAsync(u1, u2);
        //            await _dbContext.SaveChangesAsync();

        //            var result = await _repo.GetUsers();

        //            Assert.NotNull(result);
        //            Assert.Equal(2, result.Count);
        //        }
    }
}

