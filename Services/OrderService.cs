using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DTO;
using AutoMapper;
namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository,IMapper mapper)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public async Task<OrderDTO> CreateOrder(OrderDTO order)
        {
            var order1=_mapper.Map<Order>(order);
            //order1.Orderitems = order.Orderitems.Select(x => _mapper.Map<Orderitem>(x)).ToList();
            var o= await _orderRepository.CreateOrder(order1);
            return _mapper.Map<OrderDTO>(o);
        }
    }
}
