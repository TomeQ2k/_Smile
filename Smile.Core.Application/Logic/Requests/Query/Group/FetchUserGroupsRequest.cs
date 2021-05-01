using MediatR;
using Smile.Core.Application.Logic.Responses.Query.Group;

namespace Smile.Core.Application.Logic.Requests.Query.Group
{
    public class FetchUserGroupsRequest : IRequest<FetchUserGroupsResponse>
    { }
}