using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
//delete unused code and comments
//change function names to be more descriptive

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        // POST api/<OrdersController>
        [HttpPost]
        public async Task<OrderDTO> Post([FromBody] OrderDTO order)
        {
            return await _orderService.CreateOrder(order);
        }

    }
}
