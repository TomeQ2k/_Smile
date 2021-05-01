using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Logic.Requests.Command.Messenger;
using Smile.Core.Application.Logic.Responses.Command.Messenger;
using Smile.Core.Application.Services;
using Smile.Core.Application.SignalR;

namespace Smile.Core.Application.Logic.Handlers.Command.Messenger
{
    public class DeleteMessageCommand : IRequestHandler<DeleteMessageRequest, DeleteMessageResponse>
    {
        private readonly IMessenger messenger;
        private readonly IHubManager hubManager;

        public DeleteMessageCommand(IMessenger messenger, IHubManager hubManager)
        {
            this.messenger = messenger;
            this.hubManager = hubManager;
        }

        public async Task<DeleteMessageResponse> Handle(DeleteMessageRequest request, CancellationToken cancellationToken)
        {
            if (await messenger.Delete(request.MessageId))
            {
                await hubManager.Invoke(SignalrActions.ON_MESSAGE_DELETED, request.RecipientId, request.MessageId);

                return new DeleteMessageResponse();
            }

            throw new CrudException("Message has not been deleted");
        }
    }
}