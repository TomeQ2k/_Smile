using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Messenger
{
    public class ReadMessageResponse : BaseResponse
    {
        public ReadMessageResponse(Error error = null) : base(error) { }
    }
}