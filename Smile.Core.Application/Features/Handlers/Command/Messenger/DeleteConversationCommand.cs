using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Messenger;
using Smile.Core.Application.Features.Responses.Command.Messenger;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Messenger
{
    public class DeleteConversationCommand : IRequestHandler<DeleteConversationRequest, DeleteConversationResponse>
    {
        private readonly IMessenger messenger;

        public DeleteConversationCommand(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public async Task<DeleteConversationResponse> Handle(DeleteConversationRequest request, CancellationToken cancellationToken)
            => await messenger.DeleteConversation(request.RecipientId)
                ? new DeleteConversationResponse() : throw new CrudException("Conversation deleting failed");
    }
}