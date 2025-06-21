using Microsoft.AspNetCore.Mvc;
using Entities;
using Services;
using DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pettsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }


        // POST api/<OrdersController>
        [HttpPost]
        public async  Task<ActionResult<OrderDTO>> Post([FromBody] OrderDTO order)
        {
            OrderDTO newOrder= await orderService.addOrder(order);
            //return CreatedAtAction(nameof(Get), new { id = order.Id }, newOrder);
            return order;
        }

    }
}
