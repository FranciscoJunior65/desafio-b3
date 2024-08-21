
using FluentValidator;
using Microsoft.AspNetCore.Mvc;
using GetPush_Api.Domain.Services;
using GetPush_Api.Infra;
using GetPush_Api.Controllers.Response;


namespace GetPush_Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult ApiResponse(bool success, string message)
        {
            return Ok(new ApiResponse(success, message));
        }

        protected IActionResult ApiResponse(bool success, string message, object data)
        {
            return Ok(new { success, message, data });
        }

        protected IActionResult ErrorResponse(string message)
        {
            return StatusCode(500, new ApiResponse(false, message));
        }
    }
}
