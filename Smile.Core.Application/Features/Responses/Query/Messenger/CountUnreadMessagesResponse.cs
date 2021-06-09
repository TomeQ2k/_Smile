using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Query.Messenger
{
    public class CountUnreadMessagesResponse : BaseResponse
    {
        public int UnreadMessagesCount { get; set; }

        public CountUnreadMessagesResponse(Error error = null) : base(error) { }
    }
}