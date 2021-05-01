using MediatR;
using Smile.Core.Application.Logic.Responses.Query.Story;

namespace Smile.Core.Application.Logic.Requests.Query.Story
{
    public class FetchStoriesRequest : IRequest<FetchStoriesResponse> { }
}