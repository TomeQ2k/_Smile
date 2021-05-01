using MediatR;
using Smile.Core.Application.Logic.Requests.Query.Params;
using Smile.Core.Application.Logic.Responses.Query.User;

namespace Smile.Core.Application.Logic.Requests.Query.User
{
    public class GetUsersPaginationRequest : UserFiltersParams, IRequest<GetUsersPaginationResponse>
    {
    }
}