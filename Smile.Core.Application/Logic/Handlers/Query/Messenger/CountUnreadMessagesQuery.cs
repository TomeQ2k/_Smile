using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Logic.Requests.Query.Messenger;
using Smile.Core.Application.Logic.Responses.Query.Messenger;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Logic.Handlers.Query.Messenger
{
    public class CountUnreadMessagesQuery : IRequestHandler<CountUnreadMessagesRequest, CountUnreadMessagesResponse>
    {
        private readonly IReadOnlyMessenger messenger;

        public CountUnreadMessagesQuery(IReadOnlyMessenger messenger)
        {
            this.messenger = messenger;
        }

        public async Task<CountUnreadMessagesResponse> Handle(CountUnreadMessagesRequest request, CancellationToken cancellationToken)
            => new CountUnreadMessagesResponse { UnreadMessagesCount = await messenger.CountUnreadMessages() };
    }
}