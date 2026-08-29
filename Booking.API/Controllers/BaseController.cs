using Booking.Core;
using Booking.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(ServiceResponse<T> result)
        {
            if (result.Success)
            {
                if (result.Data == null)
                    return NoContent();

                var successResponse = new ApiResponse<T>(true, result.Message, result.Data);
                return Ok(successResponse);
            }

            var errorResponse = new ApiResponse<T>(false, result.Message, default);

            return result.Error switch
            {
                ErrorType.NotFound => NotFound(errorResponse),
                ErrorType.Validation => BadRequest(errorResponse),
                ErrorType.Conflict => Conflict(errorResponse),
                _ => StatusCode(500, errorResponse)
            };
        }
    }
}
