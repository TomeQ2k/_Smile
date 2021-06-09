using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Notification;
using Smile.Core.Application.Features.Requests.Query.Notification;
using Smile.Core.Application.Features.Responses.Query.Notification;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Features.Handlers.Query.Notification
{
    public class FetchNotificationsQuery : IRequestHandler<FetchNotificationsRequest, FetchNotificationsResponse>
    {
        private readonly IReadOnlyNotifier notifier;
        private readonly IMapper mapper;

        public FetchNotificationsQuery(IReadOnlyNotifier notifier, IMapper mapper)
        {
            this.notifier = notifier;
            this.mapper = mapper;
        }

        public async Task<FetchNotificationsResponse> Handle(FetchNotificationsRequest request, CancellationToken cancellationToken)
        {
            var notifications = await notifier.FetchNotifications();

            return new FetchNotificationsResponse { Notifications = mapper.Map<List<NotificationDto>>(notifications) };
        }
    }
}