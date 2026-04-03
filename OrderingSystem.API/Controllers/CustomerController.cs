using Microsoft.AspNetCore.Mvc;
using OrderingSystem.Global.DTOs.CustomerDtos;
using OrderingSystem.Global.Interfaces.Services;

namespace OrderingSystem.API.Controllers
{
    public class CustomerController : BaseController
    {

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLoginDto request)
        {
            var result = await _customerService.LoginAsync(request);
            if (result.IsSuccess)
                return Ok(new { Token = result.ReturnedObj });

            return BadRequest(new { ErrorMessage = result.Message });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CustomerRegisterDto request)
        {
            var result = await _customerService.RegisterAsync(request);
            return HandleResult(result);
        }
    }
}
