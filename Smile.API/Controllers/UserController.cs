using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Features.Requests.Query.User;
using Smile.Core.Application.Logging;

namespace Smile.API.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] GetUserRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} displayed user #{request.UserId} profile",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersPaginationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} filtered users", response.Error);

            return this.CreateResponse(response);
        }
    }
}