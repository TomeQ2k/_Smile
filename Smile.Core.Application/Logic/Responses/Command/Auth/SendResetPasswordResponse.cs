using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Auth
{
    public class SendResetPasswordResponse : BaseResponse
    {
        public SendResetPasswordResponse(Error error = null) : base(error) { }
    }
}