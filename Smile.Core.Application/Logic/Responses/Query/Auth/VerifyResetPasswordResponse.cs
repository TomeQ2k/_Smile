using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.Auth
{
    public class VerifyResetPasswordResponse : BaseResponse
    {
        public VerifyResetPasswordResponse(Error error = null) : base(error) { }
    }
}