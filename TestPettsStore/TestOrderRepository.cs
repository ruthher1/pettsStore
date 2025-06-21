using Entities;
using Moq;
using Repositories;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
    public class TestOrderRepository
    {
        [Fact]
        public async Task AddOrder_AddsOrderToContext()
        {
            var order = new Order { Id = 1, UserId = 2, OrderDate = new DateTime(2025, 1, 1), OrderSum = 100, OrderItems = [new OrderItem{Id=1,Ouantity=2,ProductId=1,OrderId=1 }] };
            var orders = new List<Order>();

            var mockContext = new Mock<PettsStoreContext>();
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders); 

            var orderRepository = new OrderRepository(mockContext.Object); 

            await orderRepository.addOrder(order);

            mockContext.Verify(m => m.Orders.AddAsync(It.Is<Order>(o => o.Id == order.Id && o.OrderSum == order.OrderSum), It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }



    }
}

