using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.Notification;
using Smile.Core.Application.Logic.Responses.Command.Notification;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.Notification
{
    public class MarkNotificationsAsReadCommand : IRequestHandler<MarkNotificationsAsReadRequest, MarkNotificationsAsReadResponse>
    {
        private readonly INotifier notifier;

        public MarkNotificationsAsReadCommand(INotifier notifier)
        {
            this.notifier = notifier;
        }

        public async Task<MarkNotificationsAsReadResponse> Handle(MarkNotificationsAsReadRequest request, CancellationToken cancellationToken)
        => await notifier.MarkAsRead() ? new MarkNotificationsAsReadResponse()
                : throw new CrudException("Marking notifications as read failed");
    }
}