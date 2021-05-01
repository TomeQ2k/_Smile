using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Logic.Requests.Command.Signalr;

namespace Smile.API.Controllers
{
    public class ConnectionController : BaseController
    {
        public ConnectionController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartConnection(StartConnectionRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} started SignalR connection #{request.ConnectionId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("close")]
        [AllowAnonymous]
        public async Task<IActionResult> CloseConnection([FromQuery] CloseConnectionRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} closed SignalR connection #{request.ConnectionId}",
                response.Error);

            return this.CreateResponse(response);
        }
    }
}