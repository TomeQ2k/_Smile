using MediatR;
using Smile.Core.Application.Features.Requests.Query.Params;
using Smile.Core.Application.Features.Responses.Query.Post;

namespace Smile.Core.Application.Features.Requests.Query.Post
{
    public class GetPostsPaginationRequest : PostFiltersParams, IRequest<GetPostsPaginationResponse>
    {
    }
}