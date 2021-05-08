using AutoMapper;
using Smile.Core.Application.Helpers;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Smile.Core.Application.Dtos.Notification;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Services;
using Smile.Core.Application.SignalR;
using Smile.Core.Domain.Entities.Notification;

namespace Smile.Infrastructure.Shared.Services
{
    public class Notifier : INotifier
    {
        private readonly IDatabase database;
        private readonly IHttpContextReader httpContextReader;
        private readonly IHubManager hubManager;
        private readonly IMapper mapper;

        public Notifier(IDatabase database, IHttpContextReader httpContextReader, IHubManager hubManager,
            IMapper mapper)
        {
            this.database = database;
            this.httpContextReader = httpContextReader;
            this.hubManager = hubManager;
            this.mapper = mapper;
        }

        public async Task Push(string message, NotificationType type = NotificationType.Other)
        {
            var notification = Notification.Create(message, httpContextReader.CurrentUserId, type);

            database.NotificationRepository.Add(notification);

            if (await database.Complete())
                await hubManager.Invoke(SignalrActions.ON_NOTIFICATION_SENT, httpContextReader.CurrentUserId,
                    mapper.Map<NotificationDto>(notification));
        }

        public async Task Push(string message, string userId, NotificationType type = NotificationType.Other)
        {
            var notification = Notification.Create(message, userId, type);

            database.NotificationRepository.Add(notification);

            if (await database.Complete())
                await hubManager.Invoke(SignalrActions.ON_NOTIFICATION_SENT, userId,
                    mapper.Map<NotificationDto>(notification));
        }

        public async Task<IEnumerable<Notification>> FetchNotifications()
            => await database.NotificationRepository.GetOrderedNotifications(httpContextReader.CurrentUserId);

        public async Task<bool> MarkAsRead()
        {
            var userNotifications =
                await database.NotificationRepository.GetWhere(n => n.UserId == httpContextReader.CurrentUserId);

            foreach (var notification in userNotifications)
                notification.MarkAsRead();

            return database.HasChanges() ? await database.Complete() : true;
        }

        public async Task<bool> Remove(string notificationId)
        {
            var notification = await database.NotificationRepository.Get(notificationId) ??
                               throw new EntityNotFoundException("Notification not found");

            database.NotificationRepository.Delete(notification);

            return await database.Complete();
        }

        public async Task<bool> Clear()
        {
            var userNotifications =
                await database.NotificationRepository.GetWhere(n => n.UserId == httpContextReader.CurrentUserId);

            database.NotificationRepository.DeleteRange(userNotifications);

            return await database.Complete();
        }

        public async Task<int> CountUnreadNotifications()
            => await database.NotificationRepository.CountUnreadNotifications(httpContextReader.CurrentUserId);
    }
}