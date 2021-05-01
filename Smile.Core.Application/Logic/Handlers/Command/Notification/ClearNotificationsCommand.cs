using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Logic.Requests.Command.Notification;
using Smile.Core.Application.Logic.Responses.Command.Notification;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.Notification
{
    public class ClearNotificationsCommand : IRequestHandler<ClearNotificationsRequest, ClearNotificationsResponse>
    {
        private readonly INotifier notifier;

        public ClearNotificationsCommand(INotifier notifier)
        {
            this.notifier = notifier;
        }

        public async Task<ClearNotificationsResponse> Handle(ClearNotificationsRequest request, CancellationToken cancellationToken)
        {
            await notifier.Clear();

            return new ClearNotificationsResponse();
        }
    }
}