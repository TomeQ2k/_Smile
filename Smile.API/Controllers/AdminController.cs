using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smile.Core.Common.Helpers;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Features.Requests.Command.Admin;
using Smile.Core.Application.Logging;

namespace Smile.API.Controllers
{
    [Authorize(Policy = Constants.AdminPolicy)]
    public class AdminController : BaseController
    {
        public AdminController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpPost("users/admitRole")]
        public async Task<IActionResult> AdmitRole(AdmitRoleRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"Admin #{HttpContext.GetCurrentUserId()} admitted role #{request.RoleId} to user #{request.UserId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("users/revokeRole")]
        public async Task<IActionResult> RevokeRole([FromQuery] RevokeRoleRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"Admin #{HttpContext.GetCurrentUserId()} revoked role #{request.RoleId} from user #{request.UserId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("users/delete")]
        [Authorize(Policy = Constants.HeadAdminPolicy)]
        public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"Admin #{HttpContext.GetCurrentUserId()} deleted user #{request.UserId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPatch("users/block")]
        public async Task<IActionResult> BlockAccount(BlockAccountRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"Admin #{HttpContext.GetCurrentUserId()} changed block status for user #{request.UserId} to: {(response.IsBlocked ? "BLOCKED" : "NOT_BLOCKED")}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPatch("users/confirm")]
        public async Task<IActionResult> ConfirmAccount(ConfirmAccountRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"Admin #{HttpContext.GetCurrentUserId()} confirmed user #{request.UserId} account",
                response.Error);

            return this.CreateResponse(response);
        }
    }
}