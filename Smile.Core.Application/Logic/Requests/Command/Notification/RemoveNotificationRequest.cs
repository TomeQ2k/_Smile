using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Notification;

namespace Smile.Core.Application.Logic.Requests.Command.Notification
{
    public class RemoveNotificationRequest : IRequest<RemoveNotificationResponse>
    {
        [Required]
        public string NotificationId { get; set; }
    }
}