using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
    public class IntegrationTestOrderRepository : IClassFixture<DatabaseFixture>
    {
        private readonly PettsStoreContext _dbContext;
        private readonly OrderRepository _orderRepository;

        public IntegrationTestOrderRepository(DatabaseFixture databaseFixture)
        {
            this._dbContext = databaseFixture.Context;
            this._orderRepository = new OrderRepository(this._dbContext);
        }



        [Fact]
        public async Task AddOrder_ValidOrder_ReturnsAddedOrder()
        {
            var category = new Category { CategoryName = "TestCategory" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var product = new Product { Name = "TestProduct", Price = 100, Description = "Test Description", CategoryId = 1 };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            var order = new Order { OrderDate = DateTime.Now, OrderSum = 100, OrderItems = new List<OrderItem> { new OrderItem { ProductId = 1, OrderId = 1, Ouantity = 5 }, new OrderItem { ProductId = 1, OrderId = 1, Ouantity = 5 } } };
            var result = await _orderRepository.addOrder(order);

            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
            Assert.Equal(order.OrderSum, result.OrderSum);
        }


        [Fact]

        public async Task AddOrder_NullOrder_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _orderRepository.addOrder(null));
        }
    }
}
