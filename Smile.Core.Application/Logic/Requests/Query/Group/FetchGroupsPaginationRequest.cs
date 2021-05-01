using MediatR;
using Smile.Core.Application.Logic.Requests.Query.Params;
using Smile.Core.Application.Logic.Responses.Query.Group;

namespace Smile.Core.Application.Logic.Requests.Query.Group
{
    public class FetchGroupsPaginationRequest : GroupFiltersParams, IRequest<FetchGroupsPaginationResponse>
    {
    }
}