using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Features.Requests.Query.Community;
using Smile.Core.Application.Features.Responses.Query.Community;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Features.Handlers.Query.Community
{
    public class CountFriendInvitesQuery : IRequestHandler<CountFriendInvitesRequest, CountFriendInvitesResponse>
    {
        private readonly IReadOnlyFriendService friendService;

        public CountFriendInvitesQuery(IReadOnlyFriendService friendService)
        {
            this.friendService = friendService;
        }

        public async Task<CountFriendInvitesResponse> Handle(CountFriendInvitesRequest request, CancellationToken cancellationToken)
            => new CountFriendInvitesResponse { FriendInvitesCount = await friendService.CountFriendInvites() };
    }
}