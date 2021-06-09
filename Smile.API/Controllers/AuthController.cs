using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smile.Core.Common.Enums;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Features.Requests.Command.Auth;
using Smile.Core.Application.Features.Requests.Query.Auth;
using Smile.Core.Application.Logging;

namespace Smile.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User {request.Email} #{response.User?.Id} logged in", response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User {request.Email} registered", response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("account/confirm")]
        public async Task<IActionResult> ConfirmAccount([FromQuery] ConfirmAccountRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{request.UserId} confirmed account", response.Error);

            return this.CreateResponse(response);
        }

        [HttpPatch("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{request.UserId} resetted password", response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("resetPassword/send")]
        public async Task<IActionResult> SendResetPassword(SendResetPasswordRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User {request.Email} sent reset password email", response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("resetPassword/verify")]
        public async Task<IActionResult> VerifyResetPassword([FromQuery] VerifyResetPasswordRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{request.UserId} verified reset password link", response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("validations")]
        public async Task<IActionResult> GetAuthValidations([FromQuery] GetAuthValidationsRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
               $"{(request.AuthValidationType == AuthValidationType.Email ? "EMAIL" : "USERNAME")} " +
               $"'{request.Content}' availability validated with status: {(response.IsAvailable ? "AVAILABLE" : "NOT AVAILABLE")}",
                response.Error);

            return this.CreateResponse(response);
        }
    }
}