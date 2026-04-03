using OrderingSystem.Global.Common;
using OrderingSystem.Global.DTOs.CustomerDtos;

namespace OrderingSystem.Global.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Result<CustomerResponseDto>> RegisterAsync(CustomerRegisterDto request);
        Task<Result<string>> LoginAsync(CustomerLoginDto request); // Returns JWT string
    }
}
