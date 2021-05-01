using System;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Dtos.Notification
{
    public class NotificationDto
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }
        public string UserId { get; set; }
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }
    }
}