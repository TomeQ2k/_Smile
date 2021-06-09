using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Auth
{
    public class RegisterResponse : BaseResponse
    {
        public RegisterResponse(Error error = null) : base(error) { }
    }
}