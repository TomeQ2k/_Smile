using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Story;
using Smile.Core.Application.Features.Responses.Command.Story;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Story
{
    public class DeleteStoryCommand : IRequestHandler<DeleteStoryRequest, DeleteStoryResponse>
    {
        private readonly IStoryManager storyManager;

        public DeleteStoryCommand(IStoryManager storyManager)
        {
            this.storyManager = storyManager;
        }

        public async Task<DeleteStoryResponse> Handle(DeleteStoryRequest request, CancellationToken cancellationToken)
            => await storyManager.DeleteStory(request.StoryId) ? new DeleteStoryResponse()
                : throw new CrudException("Story deleting failed");
    }
}