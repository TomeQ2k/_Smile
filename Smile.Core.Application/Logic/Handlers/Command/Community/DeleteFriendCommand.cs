using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Logic.Requests.Command.Community;
using Smile.Core.Application.Logic.Responses.Command.Community;
using Smile.Core.Application.Services;
using Smile.Core.Application.SignalR;

namespace Smile.Core.Application.Logic.Handlers.Command.Community
{
    public class DeleteFriendCommand : IRequestHandler<DeleteFriendRequest, DeleteFriendResponse>
    {
        private readonly IFriendService friendService;
        private readonly IHubManager hubManager;
        private readonly IHttpContextReader httpContextReader;

        public DeleteFriendCommand(IFriendService friendService, IHubManager hubManager,
            IHttpContextReader httpContextReader)
        {
            this.friendService = friendService;
            this.hubManager = hubManager;
            this.httpContextReader = httpContextReader;
        }

        public async Task<DeleteFriendResponse> Handle(DeleteFriendRequest request, CancellationToken cancellationToken)
        {
            if (await friendService.DeleteFriend(request.FriendId))
            {
                await hubManager.Invoke(SignalrActions.ON_FRIEND_DELETED, request.FriendId,
                    httpContextReader.CurrentUserId);

                return new DeleteFriendResponse();
            }

            throw new CrudException("Friend has not been deleted");
        }
    }
}