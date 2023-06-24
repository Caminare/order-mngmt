using Microsoft.AspNetCore.Mvc;
using OrderMngmt.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using OrderMngmt.Business.Models;

namespace OrderMngmt.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderModel>> GetOrders([FromQuery] PaginationFilter? paginationFilter, [FromQuery] SortFilter? sortFilter)
        {
            var orders = _orderService.GetOrders(paginationFilter, sortFilter);

            return Ok(new { Orders = orders, TotalCount = orders.Count() });
        }

        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrder(OrderModel order)
        {
            var insertedOrder = await _orderService.AddOrder(order);
            return CreatedAtAction(nameof(GetOrder), new { id = insertedOrder.Id }, insertedOrder);
        }
    }
}