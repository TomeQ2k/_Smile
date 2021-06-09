using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Notification;

namespace Smile.Core.Application.Features.Requests.Command.Notification
{
    public class RemoveNotificationRequest : IRequest<RemoveNotificationResponse>
    {
        [Required]
        public string NotificationId { get; set; }
    }
}