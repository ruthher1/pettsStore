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
        public async Task<Order> AddOrder(Order order) // פונקציה עם אות קטנה - לשנות ל-AddOrder
        {
            await _pettsStoreContext.Orders.AddAsync(order);
            await _pettsStoreContext.SaveChangesAsync();
            //RETURN OBJECT
            return order;
        }
    }
}





