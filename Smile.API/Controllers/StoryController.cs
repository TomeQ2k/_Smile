using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Logic.Requests.Command.Story;
using Smile.Core.Application.Logic.Requests.Query.Story;

namespace Smile.API.Controllers
{
    public class StoryController : BaseController
    {
        public StoryController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> FetchStories([FromQuery] FetchStoriesRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} fetched stories", response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStory([FromForm] AddStoryRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} added story #{response.Story?.Id}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("watch")]
        public async Task<IActionResult> WatchStory(WatchStoryRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} watched story #{request.StoryId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteStory([FromQuery] DeleteStoryRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} deleted story #{request.StoryId}",
                response.Error);

            return this.CreateResponse(response);
        }
    }
}