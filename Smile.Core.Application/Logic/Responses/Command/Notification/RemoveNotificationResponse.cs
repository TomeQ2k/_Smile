using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Notification
{
    public class RemoveNotificationResponse : BaseResponse
    {
        public RemoveNotificationResponse(Error error = null) : base(error) { }
    }
}