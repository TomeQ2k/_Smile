using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Logic.Requests.Command.Profile;
using Smile.Core.Application.Logic.Requests.Query.Profile;

namespace Smile.API.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile([FromQuery] GetProfileRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} displayed profile", response.Error);

            return this.CreateResponse(response);
        }

        [HttpPatch("changeUsername")]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} changed username to: {request.NewUsername}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} changed password", response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("changeEmail")]
        public async Task<IActionResult> ChangeEmail([FromQuery] ChangeEmailRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} changed email to: {request.NewEmail}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("changeEmail/send")]
        public async Task<IActionResult> SendChangeEmailCallback(GenerateChangeEmailTokenRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} generated and send change email callback to: {request.NewEmail}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("avatar/set")]
        public async Task<IActionResult> SetAvatar([FromForm] SetAvatarRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} set avatar", response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("avatar/delete")]
        public async Task<IActionResult> DeleteAvatar([FromQuery] DeleteAvatarRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} deleted avatar", response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("user/refresh")]
        public async Task<IActionResult> RefreshUserData([FromQuery] RefreshUserDataRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} refreshed their data", response.Error);

            return this.CreateResponse(response);
        }
    }
}