using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Signalr
{
    public class CloseConnectionResponse : BaseResponse
    {
        public CloseConnectionResponse(Error error = null) : base(error) { }
    }
}