using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.Notification
{
    public class CountUnreadNotificationsResponse : BaseResponse
    {
        public int UnreadNotificationsCount { get; set; }

        public CountUnreadNotificationsResponse(Error error = null) : base(error) { }
    }
}