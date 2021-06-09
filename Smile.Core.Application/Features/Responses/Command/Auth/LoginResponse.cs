using Smile.Core.Application.Dtos.Auth;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Auth
{
    public class LoginResponse : BaseResponse
    {
        public string Token { get; set; }
        public UserAuthDto User { get; set; }

        public LoginResponse(Error error = null) : base(error) { }
    }
}