using AutoMapper;
using Entities;
using Repositories;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository,IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> addOrder(OrderDTO order)
        {
            return _mapper.Map<Order,OrderDTO >(await orderRepository.addOrder(_mapper.Map <OrderDTO, Order > (order)));
        }
    }
}
