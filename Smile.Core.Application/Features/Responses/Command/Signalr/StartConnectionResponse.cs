using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Signalr
{
    public class StartConnectionResponse : BaseResponse
    {
        public StartConnectionResponse(Error error = null) : base(error) { }
    }
}