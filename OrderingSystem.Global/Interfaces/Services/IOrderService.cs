using OrderingSystem.Global.Common;
using OrderingSystem.Global.DTOs.OrderDtos;

namespace OrderingSystem.Global.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Result<OrderResponseDto>> CreateOrderAsync(int customerId, OrderCreateDto request);
        Task<Result<IEnumerable<OrderResponseDto>>> GetAllOrdersAsync();
        Task<Result<OrderResponseDto>> GetOrderByIdAsync(int id);
        Task<Result<IEnumerable<OrderResponseDto>>> GetOrdersByCustomerIdAsync(int customerId);

        // Returns a boolean Result indicating success
        Task<Result<bool>> DeleteOrderAsync(int customerId, int orderId);
    }
}
