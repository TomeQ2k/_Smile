using Microsoft.AspNetCore.Mvc;
using Smile.Core.Application.Features.Responses;

namespace Smile.Core.Application.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult CreateResponse(this ControllerBase controller, IBaseResponse response)
            => response.IsSucceeded ? (IActionResult)controller.Ok(response) : controller.StatusCode((int)response.Error.StatusCode, response);
    }
}