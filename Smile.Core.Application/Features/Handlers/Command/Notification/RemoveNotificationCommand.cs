using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Notification;
using Smile.Core.Application.Features.Responses.Command.Notification;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Notification
{
    public class RemoveNotificationCommand : IRequestHandler<RemoveNotificationRequest, RemoveNotificationResponse>
    {
        private readonly INotifier notifier;

        public RemoveNotificationCommand(INotifier notifier)
        {
            this.notifier = notifier;
        }

        public async Task<RemoveNotificationResponse> Handle(RemoveNotificationRequest request, CancellationToken cancellationToken)
            => await notifier.Remove(request.NotificationId) ? new RemoveNotificationResponse()
                : throw new CrudException("Removing notification failed");
    }
}