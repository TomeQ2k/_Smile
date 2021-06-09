using MediatR;
using Smile.Core.Application.Features.Responses.Command.Auth;

namespace Smile.Core.Application.Features.Requests.Command.Auth
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}