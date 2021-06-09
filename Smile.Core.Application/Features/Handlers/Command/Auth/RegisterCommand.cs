using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Auth;
using Smile.Core.Application.Features.Responses.Command.Auth;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Services;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Handlers.Command.Auth
{
    public class RegisterCommand : IRequestHandler<RegisterRequest, RegisterResponse>
    {
        private readonly IAuthService authService;
        private readonly IAuthValidationService authValidationService;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;

        public IConfiguration Configuration { get; }

        public RegisterCommand(IAuthService authService, IAuthValidationService authValidationService, IMapper mapper,
            IEmailSender emailSender, IConfiguration configuration)
        {
            this.authService = authService;
            this.authValidationService = authValidationService;
            this.mapper = mapper;
            this.emailSender = emailSender;

            Configuration = configuration;
        }

        public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (await authValidationService.EmailExists(request.Email))
                throw new DuplicateException("Email already exists", ErrorCodes.EmailExists);

            if (await authValidationService.UsernameExists(request.Username))
                throw new DuplicateException("Username already exists", ErrorCodes.UsernameExists);

            var signUpResult = await authService.SignUp(request.Email, request.Password, request.Username);

            if (signUpResult != null)
            {
                var registerToken = signUpResult.User?.Tokens.SingleOrDefault(t => t.Code == signUpResult.Token) ??
                                    throw new TokenException("Register token was not created",
                                        ErrorCodes.CannotGenerateToken);

                return await emailSender.Send(EmailMessages.ActivationAccountEmail(signUpResult.User?.Email,
                    signUpResult.User?.Username,
                    $"{Configuration.GetValue<string>(AppSettingsKeys.ClientAddress)}account/confirm?userId={signUpResult.User?.Id}&token={registerToken.Code}"))
                    ? new RegisterResponse()
                    : throw new ServiceException("Some error occurred during sending an email message",
                        ErrorCodes.SendEmailFailed);
            }

            throw new AuthException("Error occurred during signing up");
        }
    }
}