using System.Collections.Generic;
using Smile.Core.Application.Dtos.Notification;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Query.Notification
{
    public class FetchNotificationsResponse : BaseResponse
    {
        public List<NotificationDto> Notifications { get; set; }

        public FetchNotificationsResponse(Error error = null) : base(error) { }
    }
}