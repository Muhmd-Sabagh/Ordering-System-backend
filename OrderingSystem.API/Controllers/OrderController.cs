using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderingSystem.Global.DTOs.OrderDtos;
using OrderingSystem.Global.Interfaces.Services;
using System.Security.Claims;

namespace OrderingSystem.API.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            return HandleResult(result);
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            int customerId = GetCurrentCustomerId();
            var result = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto request)
        {
            int customerId = GetCurrentCustomerId();
            var result = await _orderService.CreateOrderAsync(customerId, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            int customerId = GetCurrentCustomerId();
            var result = await _orderService.DeleteOrderAsync(customerId, id);
            return HandleResult(result);
        }

        private int GetCurrentCustomerId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim);
        }
    }
}
