using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Story;
using Smile.Core.Application.Features.Responses.Command.Story;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Story
{
    public class WatchStoryCommand : IRequestHandler<WatchStoryRequest, WatchStoryResponse>
    {
        private readonly IStoryManager storyManager;
        private readonly IHttpContextReader httpContextReader;

        public WatchStoryCommand(IStoryManager storyManager, IHttpContextReader httpContextReader)
        {
            this.storyManager = storyManager;
            this.httpContextReader = httpContextReader;
        }

        public async Task<WatchStoryResponse> Handle(WatchStoryRequest request, CancellationToken cancellationToken)
            => await storyManager.WatchStory(request.StoryId, httpContextReader.CurrentUserId)
                ? new WatchStoryResponse()
                : throw new CrudException("Story watching failed");
    }
}