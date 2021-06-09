using MediatR;
using Smile.Core.Application.Features.Requests.Query.Params;
using Smile.Core.Application.Features.Responses.Query.User;

namespace Smile.Core.Application.Features.Requests.Query.User
{
    public class GetUsersPaginationRequest : UserFiltersParams, IRequest<GetUsersPaginationResponse>
    {
    }
}