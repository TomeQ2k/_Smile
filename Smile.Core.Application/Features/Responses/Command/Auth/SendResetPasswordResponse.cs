using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Auth
{
    public class SendResetPasswordResponse : BaseResponse
    {
        public SendResetPasswordResponse(Error error = null) : base(error) { }
    }
}