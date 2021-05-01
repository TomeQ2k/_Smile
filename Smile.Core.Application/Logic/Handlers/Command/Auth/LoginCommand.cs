using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Auth;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.Auth;
using Smile.Core.Application.Logic.Responses.Command.Auth;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.Auth
{
    public class LoginCommand : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IAuthService authService;
        private readonly IMapper mapper;

        public LoginCommand(IAuthService authService, IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var signInResult = await authService.SignIn(request.Email, request.Password);

            if (signInResult != null)
                return new LoginResponse
                {
                    Token = signInResult.Token,
                    User = mapper.Map<UserAuthDto>(signInResult.User)
                };

            throw new AuthException("Error occurred during signing in");
        }
    }
}