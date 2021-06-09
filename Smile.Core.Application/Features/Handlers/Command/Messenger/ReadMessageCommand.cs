using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Messenger;
using Smile.Core.Application.Features.Responses.Command.Messenger;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Messenger
{
    public class ReadMessageCommand : IRequestHandler<ReadMessageRequest, ReadMessageResponse>
    {
        private readonly IMessenger messenger;

        public ReadMessageCommand(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public async Task<ReadMessageResponse> Handle(ReadMessageRequest request, CancellationToken cancellationToken)
            => await messenger.ReadMessage(request.MessageId)
                ? new ReadMessageResponse() : throw new CrudException("Message has not been read");
    }
}