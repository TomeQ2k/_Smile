using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Logic.Requests.Query.LogRequests;
using Smile.Core.Common.Helpers;

namespace Smile.API.Controllers
{
    [Authorize(Policy = Constants.AdminPolicy)]
    public class LogController : BaseController
    {
        public LogController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetLogs([FromQuery] GetLogsPaginationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"Admin #{HttpContext.GetCurrentUserId()} filtered logs", response.Error);

            return this.CreateResponse(response);
        }
    }
}