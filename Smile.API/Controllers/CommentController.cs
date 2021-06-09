using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Features.Requests.Command.Comment;
using Smile.Core.Application.Logging;

namespace Smile.API.Controllers
{
    public class CommentController : BaseController
    {
        public CommentController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateComment(CreateCommentRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} created comment #{response.Comment?.Id} in post #{request.PostId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateComment(UpdateCommentRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} updated comment #{request.CommentId} in post #{request.PostId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteComment([FromQuery] DeleteCommentRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} deleted comment #{request.CommentId}",
                response.Error);

            return this.CreateResponse(response);
        }
    }
}