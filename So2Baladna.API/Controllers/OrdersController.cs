using Ecom.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Interfaces;
using So2Baladna.Infrastructure.Repositories.Services;
using System.Security.Claims;

namespace So2Baladna.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpPost("create-order")]
        public async Task<IActionResult> Create(OrderDto orderDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Email claim not found.");
            }

            var createdOrder = await orderService.CreateOrderAsync(orderDto, email);

            return Ok(createdOrder);
        }
        [HttpGet("get-orders-for-user")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrders()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var order = await orderService.GetOrdersForUserAsync(email!);
            return Ok(order);
        }


        [HttpGet("get-order-by-id/{id}")]
        public async Task<ActionResult<OrderToReturnDTO>> getOrderById(int id)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var order = await orderService.GetOrderByIdAsync(id, email!);
            return Ok(order);
        }


        [HttpGet("get-delivery")]
        public async Task<ActionResult> GetDeliver()
        => Ok(await orderService.GetDeliveryMethodAsync());
    }
}
