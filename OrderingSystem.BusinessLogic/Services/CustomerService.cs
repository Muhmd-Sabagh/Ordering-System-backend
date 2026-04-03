using AutoMapper;
using Microsoft.Extensions.Logging;
using OrderingSystem.BusinessLogic.Utils;
using OrderingSystem.Global.Common;
using OrderingSystem.Global.DTOs.CustomerDtos;
using OrderingSystem.Global.Entities;
using OrderingSystem.Global.Interfaces.Services;
using OrderingSystem.Global.Interfaces.UnitOfWork;

namespace OrderingSystem.BusinessLogic.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CustomerService> logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> LoginAsync(CustomerLoginDto request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                // Finde Customer
                var customer = (await _unitOfWork.Customers.FindAsync(c => c.Username == request.Username)).FirstOrDefault();

                if (customer == null || !BCrypt.Net.BCrypt.Verify(request.Password, customer.PasswordHash))
                    return Result<string>.Failure("Invalid username or password.");

                // Generate JWT Token
                var token = SystemTokenHandler.GenerateToken(customer);
                return Result<string>.Success(token);
            });
        }

        public async Task<Result<CustomerResponseDto>> RegisterAsync(CustomerRegisterDto request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                // Check if the customer already exists
                var existingCustomer = await _unitOfWork.Customers.FindAsync(c => c.Username == request.Username);
                if (existingCustomer.Any())
                    return Result<CustomerResponseDto>.Failure("Username already exists.");

                // Hash the password mapping
                var customer = _mapper.Map<Customer>(request);
                customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                // Add to database
                await _unitOfWork.Customers.AddAsync(customer);
                await _unitOfWork.CompleteAsync();

                var response = _mapper.Map<CustomerResponseDto>(customer);
                return Result<CustomerResponseDto>.Success(response);
            });
        }
    }
}
