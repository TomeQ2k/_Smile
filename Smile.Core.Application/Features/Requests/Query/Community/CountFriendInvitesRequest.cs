using MediatR;
using Smile.Core.Application.Features.Responses.Query.Community;

namespace Smile.Core.Application.Features.Requests.Query.Community
{
    public class CountFriendInvitesRequest : IRequest<CountFriendInvitesResponse>
    { }
}