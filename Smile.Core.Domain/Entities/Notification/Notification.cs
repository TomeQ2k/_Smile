using System;
using Smile.Core.Common.Enums;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Notification
{
    public class Notification
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Message { get; protected set; }
        public DateTime DateSent { get; protected set; } = DateTime.Now;
        public string UserId { get; protected set; }
        public bool IsRead { get; protected set; }
        public NotificationType Type { get; protected set; } = NotificationType.Other;

        public virtual User User { get; protected set; }

        public static Notification Create(string message, string userId, NotificationType type = NotificationType.Other)
            => new Notification { Message = message, UserId = userId, Type = type };

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}