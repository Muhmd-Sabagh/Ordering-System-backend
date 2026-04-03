using Microsoft.AspNetCore.Mvc;
using OrderingSystem.Global.Common;

namespace OrderingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        // Wrapping Result<T> into HTTP Status Codes
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.ReturnedObj);

            return BadRequest(new { ErrorMessage = result.Message });
        }
    }
}
