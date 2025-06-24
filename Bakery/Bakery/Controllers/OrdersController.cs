using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
//delete unused code
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrdersServices ordersServices;//_ordersServices;
        public OrdersController(IOrdersServices _ordersServices)
        {
            ordersServices = _ordersServices;
        }

        // GET: api/<OrdersController>//
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            return await ordersServices.getOrders(); // Suggestion: getOrders => GetOrders (PascalCase)
        }

        // POST api/<OrdersController>//
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody]OrderDto order) 
        {
            await ordersServices.addOrder(order); // Suggestion: addOrder => AddOrder (PascalCase)
            return Ok(order); 
        }


    }
}
