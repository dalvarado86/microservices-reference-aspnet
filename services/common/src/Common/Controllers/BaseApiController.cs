using Commons.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commons.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(Response<T> response)
        {
            if (response == null)
            {
                return NotFound();
            }

            if (response.IsSuccess && response.Value != null)
            {
                return Ok(response.Value);
            }

            if (response.IsSuccess && response.Value == null)
            {
                return NotFound();
            }

            return BadRequest(response.Error);
        }
    }
}
