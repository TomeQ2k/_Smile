using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.Story;
using Smile.Core.Application.Logic.Responses.Command.Story;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.Story
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