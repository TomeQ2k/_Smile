using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Notification
{
    public class ClearNotificationsResponse : BaseResponse
    {
        public ClearNotificationsResponse(Error error = null) : base(error) { }
    }
}