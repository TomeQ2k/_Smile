using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Auth
{
    public class RegisterResponse : BaseResponse
    {
        public RegisterResponse(Error error = null) : base(error) { }
    }
}