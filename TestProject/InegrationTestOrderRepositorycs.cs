using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class IntegrationTestOrderRepository : IClassFixture<DatabaseFixture>
    {
        private readonly _326059268_ShopApiContext _dbContext;
        private readonly OrderRepository _orderRepository;

        public IntegrationTestOrderRepository(DatabaseFixture fixture)
        {
            _dbContext = fixture.DbContext;
            _orderRepository = new OrderRepository(_dbContext);
        }

        [Fact]
        public async Task CreateOrder_Should_Add_Order_With_Orderitems_And_User()
        {
            // Arrange - יצירת משתמש
            var user = new User
            {
                UserName = "orderuser",
                FirstName = "Order",
                LastName = "User",
                Password = "1234"
            };
            await _dbContext.Users.AddAsync(user);

            // יצירת קטגוריה חוקית
            var category = new Catgory
            {
                CatgoryName = "Test Category"
            };
            await _dbContext.Catgories.AddAsync(category);
            await _dbContext.SaveChangesAsync(); // שומר את user + category

            // יצירת מוצר עם קטגוריה
            var product = new Product
            {
                ProductName = "Test Product",
                Price = 75,
                CatgoryId = category.CatgoryId
            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            // יצירת הזמנה עם פריט
            var order = new Order
            {
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                OrderSum = 150,
                UserId = user.Id,
                Orderitems = new List<Orderitem>
        {
            new Orderitem
            {
                ProductId = product.ProductId,
                Quntity = 2
            }
        }
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            // Act – שליפת ההזמנה עם הפריטים והמשתמש
            var fromDb = await _dbContext.Orders
                .Include(o => o.Orderitems)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

            // Assert
            Assert.NotNull(fromDb);
            Assert.Equal(user.Id, fromDb.UserId);
            Assert.NotNull(fromDb.User);
            Assert.Single(fromDb.Orderitems);

            var orderItem = fromDb.Orderitems.First();
            Assert.Equal(product.ProductId, orderItem.ProductId);
            Assert.Equal(2, orderItem.Quntity);
        }

        //[Fact]
        //public async Task CreateOrder_Should_Throw_When_User_Not_Exist()
        //{
        //    // Arrange
        //    var order = new Order
        //    {
        //        OrderDate = DateOnly.FromDateTime(DateTime.Now),
        //        OrderSum = 200,
        //        UserId = 9999, // יוזר שלא קיים
        //        Orderitems = new List<Orderitem>()
        //    };

        //    // Act & Assert
        //    await Assert.ThrowsAnyAsync<DbUpdateException>(async () =>
        //    {
        //        await _orderRepository.CreateOrder(order);
        //    });
        //}

        //[Fact]
        //public async Task CreateOrder_With_No_Orderitems_Should_Succeed()
        //{
        //    // Arrange
        //    var user = new User
        //    {
        //        UserName = "noitemsuser",
        //        FirstName = "No",
        //        LastName = "Items",
        //        Password = "test"
        //    };
        //    await _dbContext.Users.AddAsync(user);
        //    await _dbContext.SaveChangesAsync();

        //    var order = new Order
        //    {
        //        OrderDate = DateOnly.FromDateTime(DateTime.Now),
        //        OrderSum = 0,
        //        UserId = user.Id,
        //        Orderitems = new List<Orderitem>() // אין פריטים
        //    };

        //    // Act
        //    var result = await _orderRepository.CreateOrder(order);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.True(result.OrderId > 0);

        //    var fromDb = await _dbContext.Orders
        //        .Include(o => o.Orderitems)
        //        .FirstOrDefaultAsync(o => o.OrderId == result.OrderId);

        //    Assert.NotNull(fromDb);
        //    Assert.Empty(fromDb.Orderitems);
        //}

        [Fact]
        public async Task CreateOrder_Should_Throw_When_User_Not_Exist()
        {
            // Arrange - אין יצירת יוזר
            var order = new Order
            {
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                OrderSum = 200,
                UserId = 9999, // מזהה יוזר שלא קיים
                Orderitems = new List<Orderitem>()
            };

            // Act & Assert
            await Assert.ThrowsAnyAsync<DbUpdateException>(async () =>
            {
                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync(); // כאן נזרקת השגיאה
            });
        }
        [Fact]
        public async Task CreateOrder_With_No_Orderitems_Should_Succeed()
        {
            // Arrange - יוזר קיים
            var user = new User
            {
                UserName = "noitemsuser",
                FirstName = "No",
                LastName = "Items",
                Password = "test"
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var order = new Order
            {
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                OrderSum = 0,
                UserId = user.Id,
                Orderitems = new List<Orderitem>() // אין פריטים
            };

            // Act
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            // Assert
            var fromDb = await _dbContext.Orders
                .Include(o => o.Orderitems)
                .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

            Assert.NotNull(fromDb);
            Assert.Equal(user.Id, fromDb.UserId);
            Assert.Empty(fromDb.Orderitems);
        }


    }
}