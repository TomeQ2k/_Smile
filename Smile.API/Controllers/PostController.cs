using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Logic.Requests.Command.Post;
using Smile.Core.Application.Logic.Requests.Query.Post;

namespace Smile.API.Controllers
{
    public class PostController : BaseController
    {
        public PostController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPost([FromQuery] GetPostRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} displayed post #{request.PostId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPosts([FromQuery] GetPostsPaginationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} filtered posts", response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} created post #{response.Post?.Id}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePost([FromForm] UpdatePostRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} updated post #{response.Post?.Id}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePost([FromQuery] DeletePostRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} deleted post #{request.PostId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPut("like")]
        public async Task<IActionResult> LikePost(LikePostRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} {(response.LikeCreated ? "LIKED" : "UNLIKED")} post #{request.PostId}",
                response.Error);

            return this.CreateResponse(response);
        }
    }
}