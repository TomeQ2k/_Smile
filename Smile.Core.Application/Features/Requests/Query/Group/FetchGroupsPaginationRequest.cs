using MediatR;
using Smile.Core.Application.Features.Requests.Query.Params;
using Smile.Core.Application.Features.Responses.Query.Group;

namespace Smile.Core.Application.Features.Requests.Query.Group
{
    public class FetchGroupsPaginationRequest : GroupFiltersParams, IRequest<FetchGroupsPaginationResponse>
    {
    }
}