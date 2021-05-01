using MediatR;
using Smile.Core.Application.Logic.Requests.Query.Params;
using Smile.Core.Application.Logic.Responses.Query.Post;

namespace Smile.Core.Application.Logic.Requests.Query.Post
{
    public class GetPostsPaginationRequest : PostFiltersParams, IRequest<GetPostsPaginationResponse>
    {
    }
}