using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Story;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Story;
using Smile.Core.Application.Features.Responses.Command.Story;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Story
{
    public class AddStoryCommand : IRequestHandler<AddStoryRequest, AddStoryResponse>
    {
        private readonly IStoryManager storyManager;
        private readonly IMapper mapper;

        public AddStoryCommand(IStoryManager storyManager, IMapper mapper)
        {
            this.storyManager = storyManager;
            this.mapper = mapper;
        }

        public async Task<AddStoryResponse> Handle(AddStoryRequest request, CancellationToken cancellationToken)
        {
            var addedStory = await storyManager.AddStory(request.Photo);

            return addedStory != null ? new AddStoryResponse { Story = mapper.Map<StoryDto>(addedStory) }
                : throw new CrudException("Story adding failed");
        }
    }
}