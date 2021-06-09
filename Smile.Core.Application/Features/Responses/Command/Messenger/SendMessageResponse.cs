using Smile.Core.Application.Dtos.Messenger;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Messenger
{
    public class SendMessageResponse : BaseResponse
    {
        public MessageDto Message { get; set; }

        public SendMessageResponse(Error error = null) : base(error) { }
    }
}