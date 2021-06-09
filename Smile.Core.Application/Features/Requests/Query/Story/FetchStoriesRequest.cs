using MediatR;
using Smile.Core.Application.Features.Responses.Query.Story;

namespace Smile.Core.Application.Features.Requests.Query.Story
{
    public class FetchStoriesRequest : IRequest<FetchStoriesResponse> { }
}