using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Messenger;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Messenger;
using Smile.Core.Application.Features.Responses.Command.Messenger;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Services;
using Smile.Core.Application.SignalR;

namespace Smile.Core.Application.Features.Handlers.Command.Messenger
{
    public class SendMessageCommand : IRequestHandler<SendMessageRequest, SendMessageResponse>
    {
        private readonly IMessenger messenger;
        private readonly IMapper mapper;
        private readonly IHubManager hubManager;

        public SendMessageCommand(IMessenger messenger, IMapper mapper, IHubManager hubManager)
        {
            this.messenger = messenger;
            this.mapper = mapper;
            this.hubManager = hubManager;
        }

        public async Task<SendMessageResponse> Handle(SendMessageRequest request, CancellationToken cancellationToken)
        {
            var messageSent = await messenger.Send(request.RecipientId, request.Text);

            if (messageSent != null)
            {
                await hubManager.Invoke(SignalrActions.ON_MESSAGE_RECEIVED, request.RecipientId, mapper.Map<MessageDto>(messageSent));

                return new SendMessageResponse { Message = mapper.Map<MessageDto>(messageSent) };
            }

            throw new CrudException("Message has not been sent");
        }
    }
}