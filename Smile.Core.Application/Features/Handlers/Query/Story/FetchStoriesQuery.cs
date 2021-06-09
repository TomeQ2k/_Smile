using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Story;
using Smile.Core.Application.Features.Requests.Query.Story;
using Smile.Core.Application.Features.Responses.Query.Story;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Features.Handlers.Query.Story
{
    public class FetchStoriesQuery : IRequestHandler<FetchStoriesRequest, FetchStoriesResponse>
    {
        private readonly IReadOnlyStoryManager storyManager;
        private readonly IMapper mapper;
        private readonly IHttpContextReader httpContextReader;

        public FetchStoriesQuery(IReadOnlyStoryManager storyManager, IMapper mapper,
            IHttpContextReader httpContextReader)
        {
            this.storyManager = storyManager;
            this.mapper = mapper;
            this.httpContextReader = httpContextReader;
        }

        public async Task<FetchStoriesResponse> Handle(FetchStoriesRequest request, CancellationToken cancellationToken)
        {
            var stories = await storyManager.FetchStories();

            var storiesToReturn = mapper.Map<List<StoryDto>>(stories);

            var storyWrappers = storyManager.CreateStoryWrappers(httpContextReader.CurrentUserId, storiesToReturn);

            return new FetchStoriesResponse {Stories = storyWrappers};
        }
    }
}