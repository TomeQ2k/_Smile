using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Logic.Requests.Command.Group;
using Smile.Core.Application.Logic.Requests.Query.Group;

namespace Smile.API.Controllers
{
    public class GroupController : BaseController
    {
        public GroupController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetGroup([FromQuery] GetGroupRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} fetched group #{response?.Group?.Id} called: {response?.Group?.Name?.ToUpper()}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FetchGroups([FromQuery] FetchGroupsPaginationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} fetched groups", response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGroup([FromForm] CreateGroupRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} created group #{response?.Group?.Id} called: {response?.Group?.Name?.ToUpper()}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinGroup(JoinGroupRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} joined group #{request.GroupId} with status: {(response.Member.IsAccepted ? "ACCEPTED" : "WAITING")}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("user/groups")]
        public async Task<IActionResult> FetchUserGroups([FromQuery] FetchUserGroupsRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} fetched their groups", response.Error);

            return this.CreateResponse(response);
        }
    }
}