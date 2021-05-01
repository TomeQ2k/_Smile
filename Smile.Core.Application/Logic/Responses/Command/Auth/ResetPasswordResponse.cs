using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Auth
{
    public class ResetPasswordResponse : BaseResponse
    {
        public ResetPasswordResponse(Error error = null) : base(error) { }
    }
}