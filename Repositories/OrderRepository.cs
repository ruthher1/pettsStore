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
        PettsStoreContext _pettsStoreContext;
        public OrderRepository(PettsStoreContext pettsStoreContext)
        {
            _pettsStoreContext = pettsStoreContext;
        }
        public async Task<Order> addOrder(Order order)
        {
            await _pettsStoreContext.Orders.AddAsync(order);
            await _pettsStoreContext.SaveChangesAsync();
            return order;
        }

    }
}





