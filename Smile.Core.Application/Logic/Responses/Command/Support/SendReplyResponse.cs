using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Support
{
    public class SendReplyResponse : BaseResponse
    {
        public ReplyDto Reply { get; set; }

        public SendReplyResponse(Error error = null) : base(error) { }
    }
}