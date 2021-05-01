using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Logic.Requests.Query.Notification;
using Smile.Core.Application.Logic.Responses.Query.Notification;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Logic.Handlers.Query.Notification
{
    public class CountUnreadNotificationsQuery : IRequestHandler<CountUnreadNotificationsRequest, CountUnreadNotificationsResponse>
    {
        private readonly IReadOnlyNotifier notifier;

        public CountUnreadNotificationsQuery(IReadOnlyNotifier notifier)
        {
            this.notifier = notifier;
        }

        public async Task<CountUnreadNotificationsResponse> Handle(CountUnreadNotificationsRequest request, CancellationToken cancellationToken)
            => new CountUnreadNotificationsResponse { UnreadNotificationsCount = await notifier.CountUnreadNotifications() };
    }
}