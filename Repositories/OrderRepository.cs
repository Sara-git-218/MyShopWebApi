using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
   public class OrderRepository : IOrderRepository
    {
        _326059268_ShopApiContext DBcontext;//_dbContext
        public OrderRepository(_326059268_ShopApiContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public async Task<Order> CreateOrder(Order order)
        {

            await DBcontext.Orders.AddAsync(order);
            await DBcontext.SaveChangesAsync();
            return await Task.FromResult(order);//return order;

        }
    }
}
