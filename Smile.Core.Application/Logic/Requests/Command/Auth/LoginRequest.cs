using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Auth;

namespace Smile.Core.Application.Logic.Requests.Command.Auth
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}