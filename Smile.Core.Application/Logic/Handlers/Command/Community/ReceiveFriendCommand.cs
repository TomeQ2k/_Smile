using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Community;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Logic.Requests.Command.Community;
using Smile.Core.Application.Logic.Responses.Command.Community;
using Smile.Core.Application.Services;
using Smile.Core.Application.SignalR;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Logic.Handlers.Command.Community
{
    public class ReceiveFriendCommand : IRequestHandler<ReceiveFriendRequest, ReceiveFriendResponse>
    {
        private readonly IFriendService friendService;
        private readonly IMapper mapper;
        private readonly IHubManager hubManager;
        private readonly INotifier notifier;
        private readonly IHttpContextReader httpContextReader;

        public ReceiveFriendCommand(IFriendService friendService, IMapper mapper, IHubManager hubManager,
            INotifier notifier, IHttpContextReader httpContextReader)
        {
            this.friendService = friendService;
            this.mapper = mapper;
            this.hubManager = hubManager;
            this.notifier = notifier;
            this.httpContextReader = httpContextReader;
        }

        public async Task<ReceiveFriendResponse> Handle(ReceiveFriendRequest request,
            CancellationToken cancellationToken)
        {
            var receiveResult =
                await friendService.Receive(request.SenderId, request.RecipientId, accepted: request.Accepted);

            var friendUpdated = receiveResult?.Friend != null ? mapper.Map<FriendDto>(receiveResult.Friend) : null;

            string currentUserId = httpContextReader.CurrentUserId;

            if (receiveResult != null)
            {
                await hubManager.Invoke(SignalrActions.ON_FRIEND_RECEIVED,
                    currentUserId == request.SenderId ? request.RecipientId : request.SenderId, request);

                if (request.Accepted)
                    await notifier.Push(currentUserId == friendUpdated.SenderId
                            ? NotificationMessages.FriendAcceptedNotification(friendUpdated.RecipientName)
                            : NotificationMessages.FriendAcceptedNotification(friendUpdated.SenderName),
                        currentUserId == friendUpdated.SenderId
                            ? friendUpdated.RecipientId
                            : friendUpdated.SenderId,
                        NotificationType.FriendAccepted);

                return new ReceiveFriendResponse
                    {FriendAccepted = receiveResult.FriendAccepted, Friend = friendUpdated};
            }

            throw new CrudException("Friend invite has not been accepted");
        }
    }
}