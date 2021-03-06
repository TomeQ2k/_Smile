using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Notification
{
    public class MarkNotificationsAsReadResponse : BaseResponse
    {
        public MarkNotificationsAsReadResponse(Error error = null) : base(error) { }
    }
}