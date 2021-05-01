using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Messenger
{
    public class DeleteMessageResponse : BaseResponse
    {
        public DeleteMessageResponse(Error error = null) : base(error) { }
    }
}