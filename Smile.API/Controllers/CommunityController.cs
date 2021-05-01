using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Logic.Requests.Command.Community;
using Smile.Core.Application.Logic.Requests.Query.Community;

namespace Smile.API.Controllers
{
    public class CommunityController : BaseController
    {
        public CommunityController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpGet("friends")]
        public async Task<IActionResult> GetFriends([FromQuery] GetFriendsPaginationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} filtered their friends", response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("friend/invite")]
        public async Task<IActionResult> InviteFriend(InviteFriendRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} invited to friends user #{request.RecipientId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPut("friend/receive")]
        public async Task<IActionResult> ReceiveFriend(ReceiveFriendRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} received invitation to friends from user #{request.SenderId} and {(response.FriendAccepted ? "ACCEPTED" : "DENIED")}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("friend/delete")]
        public async Task<IActionResult> DeleteFriend([FromQuery] DeleteFriendRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} delete friend #{request.FriendId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("friends/invites/count")]
        public async Task<IActionResult> CountFriendInvites([FromQuery] CountFriendInvitesRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} has {response?.FriendInvitesCount} friend invites",
                response.Error);

            return this.CreateResponse(response);
        }
    }
}