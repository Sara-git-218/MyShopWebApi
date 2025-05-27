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
            // Arrange
            // צור יוזר (כי יש ForeignKey)
            var user = new User
            {
                UserName = "orderuser",
                FirstName = "Order",
                LastName = "User",
                Password = "1234"
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            // צור Order עם פריט אחד לפחות
            var order = new Order
            {
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                OrderSum = 150,
                UserId = user.UserId,
                Orderitems = new List<Orderitem>
                {
                    new Orderitem
                    {
                        // מלא כאן שדות חובה לפי המבנה שלך, למשל:
                        // ProductId = 1,
                        // Quantity = 2,
                        // Price = 75
                    }
                }
            };

            // Act
            var result = await _orderRepository.CreateOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.OrderId > 0);

            var fromDb = await _dbContext.Orders
                .Include(o => o.Orderitems)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.OrderId == result.OrderId);

            Assert.NotNull(fromDb);
            Assert.Equal(user.UserId, fromDb.UserId);
            Assert.Equal(1, fromDb.Orderitems.Count);
            Assert.NotNull(fromDb.User);
        }

        [Fact]
        public async Task CreateOrder_Should_Throw_When_User_Not_Exist()
        {
            // Arrange
            var order = new Order
            {
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                OrderSum = 200,
                UserId = 9999, // יוזר שלא קיים
                Orderitems = new List<Orderitem>()
            };

            // Act & Assert
            await Assert.ThrowsAnyAsync<DbUpdateException>(async () =>
            {
                await _orderRepository.CreateOrder(order);
            });
        }

        [Fact]
        public async Task CreateOrder_With_No_Orderitems_Should_Succeed()
        {
            // Arrange
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
                UserId = user.UserId,
                Orderitems = new List<Orderitem>() // אין פריטים
            };

            // Act
            var result = await _orderRepository.CreateOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.OrderId > 0);

            var fromDb = await _dbContext.Orders
                .Include(o => o.Orderitems)
                .FirstOrDefaultAsync(o => o.OrderId == result.OrderId);

            Assert.NotNull(fromDb);
            Assert.Empty(fromDb.Orderitems);
        }
    }
}