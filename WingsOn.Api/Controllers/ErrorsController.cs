using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models;
using WingsOn.Api.Models.Responses;

namespace WingsOn.Api.Controllers
{
    /// <summary>
    /// Manages the errors that were not managed by any controller, like 404 Not found error.
    /// </summary>
    [Route("errors")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        /// <summary>
        /// Manage the 404 Not Found error.
        /// </summary>
        /// <returns></returns>
        [HttpGet("404")]
        public IActionResult GetError404(int code)
        {
            return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_404, "The page you're looking for does not exist! Sorry, this should be a nice page."));
        }

        /// <summary>
        /// Manage the 404 Not Found error.
        /// </summary>
        /// <returns></returns>
        [HttpPost("404")]
        public IActionResult PostError404(int code)
        {
            return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_404, "The page you're looking for does not exist! Sorry, this should be a nice page."));
        }

        /// <summary>
        /// Manage the 404 Not Found error.
        /// </summary>
        /// <returns></returns>
        [HttpPut ("404")]
        public IActionResult PutError404(int code)
        {
            return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_404, "The page you're looking for does not exist! Sorry, this should be a nice page."));
        }

        /// <summary>
        /// Manage all the rest of the errors.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{code:int}")]
        public IActionResult GetErrorUnknown(int code)
        {
            return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_UNKNOWN, "Unknown error, we're working to find out what happened!"));
        }

        /// <summary>
        /// Manage all the rest of the errors.
        /// </summary>
        /// <returns></returns>
        [HttpPost("{code:int}")]
        public IActionResult PostErrorUnknown(int code)
        {
            return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_UNKNOWN, "Unknown error, we're working to find out what happened!"));
        }

        /// <summary>
        /// Manage all the rest of the errors.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{code:int}")]
        public IActionResult PutErrorUnknown(int code)
        {
            return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_UNKNOWN, "Unknown error, we're working to find out what happened!"));
        }
    }
}