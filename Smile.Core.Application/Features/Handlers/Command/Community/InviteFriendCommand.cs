using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Community;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Community;
using Smile.Core.Application.Features.Responses.Command.Community;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Services;
using Smile.Core.Application.SignalR;

namespace Smile.Core.Application.Features.Handlers.Command.Community
{
    public class InviteFriendCommand : IRequestHandler<InviteFriendRequest, InviteFriendResponse>
    {
        private readonly IFriendService friendService;
        private readonly IMapper mapper;
        private readonly IHubManager hubManager;

        public InviteFriendCommand(IFriendService friendService, IMapper mapper, IHubManager hubManager)
        {
            this.friendService = friendService;
            this.mapper = mapper;
            this.hubManager = hubManager;
        }

        public async Task<InviteFriendResponse> Handle(InviteFriendRequest request, CancellationToken cancellationToken)
        {
            var friendCreated = await friendService.Invite(request.RecipientId);

            if (friendCreated != null)
            {
                var friendToReturn = mapper.Map<FriendDto>(friendCreated);

                await hubManager.Invoke(SignalrActions.ON_FRIEND_INVITED, request.RecipientId, friendToReturn);

                return new InviteFriendResponse { Friend = friendToReturn };
            }

            throw new CrudException("Friend invite has not been sent");
        }
    }
}