using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Features.Requests.Command.GroupManager;
using Smile.Core.Application.Features.Requests.Query.GroupManager;
using Smile.Core.Application.Logging;

namespace Smile.API.Controllers
{
    public class GroupManagerController : BaseController
    {
        public GroupManagerController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpPost("members/invite")]
        public async Task<IActionResult> InviteMember(InviteMemberRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} invited user #{request.UserId} to group #{request.GroupId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPut("members/accept")]
        public async Task<IActionResult> AcceptMember(AcceptMemberRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} {(request.Accept ? "ACCEPTED" : "DENIED")} member invitation for user #{request.UserId} to group #{request.GroupId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("members/kick")]
        public async Task<IActionResult> KickMember([FromQuery] KickMemberRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} kicked user #{request.UserId} from group #{request.GroupId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("members/leave")]
        public async Task<IActionResult> LeaveGroup([FromQuery] LeaveGroupRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} leaved group #{request.GroupId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateGroup([FromForm] UpdateGroupRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} updated group #{request.GroupId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteGroup([FromQuery] DeleteGroupRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} deleted group #{request.GroupId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPatch("members/setModerator")]
        public async Task<IActionResult> SetModerator(SetModeratorRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} {(request.IsModerator ? "ASSIGNED" : "REMOVED")} moderator for user #{request.UserId} in group #{request.GroupId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("members/canInvite")]
        public async Task<IActionResult> CanInviteMember([FromQuery] CanInviteMemberRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} checked that user :{request.Username} {(response.CanInvite ? "CAN" : "CANNOT")} be invited to group #{request.GroupId}",
                response.Error);

            return this.CreateResponse(response);
        }
    }
}