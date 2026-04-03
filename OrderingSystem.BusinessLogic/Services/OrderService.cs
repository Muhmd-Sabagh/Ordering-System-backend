using AutoMapper;
using Microsoft.Extensions.Logging;
using OrderingSystem.Global.Common;
using OrderingSystem.Global.DTOs.OrderDtos;
using OrderingSystem.Global.Entities;
using OrderingSystem.Global.Interfaces.Services;
using OrderingSystem.Global.Interfaces.UnitOfWork;

namespace OrderingSystem.BusinessLogic.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderService> logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<OrderResponseDto>> CreateOrderAsync(int customerId, OrderCreateDto request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
                if (customer == null)
                    return Result<OrderResponseDto>.Failure("Customer not found.");

                if (customer.BannedUntil.HasValue && customer.BannedUntil.Value > DateTime.UtcNow)
                    return Result<OrderResponseDto>.Failure($"Your are banned from making orders until {customer.BannedUntil.Value:yyyy-MM-dd HH:mm:ss}.");

                var order = _mapper.Map<Order>(request);
                order.CustomerId = customerId;

                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.CompleteAsync();

                var response = _mapper.Map<OrderResponseDto>(order);
                return Result<OrderResponseDto>.Success(response);
            });
        }

        public async Task<Result<IEnumerable<OrderResponseDto>>> GetAllOrdersAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var orders = await _unitOfWork.Orders.GetAllAsync();
                var response = _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
                return Result<IEnumerable<OrderResponseDto>>.Success(response);
            });
        }

        public async Task<Result<OrderResponseDto>> GetOrderByIdAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(id);
                if (order == null)
                    return Result<OrderResponseDto>.Failure("Order not found.");
                var response = _mapper.Map<OrderResponseDto>(order);
                return Result<OrderResponseDto>.Success(response);
            });
        }

        public async Task<Result<IEnumerable<OrderResponseDto>>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var orders = await _unitOfWork.Orders.FindAsync(o => o.CustomerId == customerId);
                var response = _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
                return Result<IEnumerable<OrderResponseDto>>.Success(response);
            });
        }

        public async Task<Result<bool>> DeleteOrderAsync(int customerId, int orderId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

                if (order == null || order.CustomerId != customerId)
                    return Result<bool>.Failure("Order not found or not yours.");

                // Soft delete
                _unitOfWork.Orders.Delete(order);
                await _unitOfWork.CompleteAsync();

                // Ban customer if this is their 3rd order deletion within the day
                var today = DateTime.UtcNow.Date;

                if (order.CreatedAt.Date == today)
                {
                    var ordersDeletedToday = await _unitOfWork.Orders.FindWithDeletedAsync(o =>
                    o.CustomerId == customerId &&
                    o.IsDeleted == true &&
                    o.CreatedAt.Date == today &&
                    o.DeletedAt.HasValue &&
                    o.DeletedAt.Value.Date == today);

                    if (ordersDeletedToday.Count() >= 3)
                    {
                        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
                        if (customer != null)
                        {
                            customer.BannedUntil = DateTime.UtcNow.AddHours(6);
                            _unitOfWork.Customers.Update(customer);
                            await _unitOfWork.CompleteAsync();

                            _logger.LogWarning($"Customer Id: {customer.Id}, Name: {customer.FirstName} {customer.LastName} banned for 6 hours");
                        }
                    }
                }

                return Result<bool>.Success(true);
            });
        }
    }
}
