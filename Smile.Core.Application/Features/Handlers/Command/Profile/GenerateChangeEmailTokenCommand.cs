using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Profile;
using Smile.Core.Application.Features.Responses.Command.Profile;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Services;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Handlers.Command.Profile
{
    public class GenerateChangeEmailTokenCommand : IRequestHandler<GenerateChangeEmailTokenRequest, GenerateChangeEmailTokenResponse>
    {
        private readonly IProfileService profileService;
        private readonly IAuthValidationService authValidationService;
        private readonly IEmailSender emailSender;

        public IConfiguration Configuration { get; }

        public GenerateChangeEmailTokenCommand(IProfileService profileService, IAuthValidationService authValidationService, IEmailSender emailSender, IConfiguration configuration)
        {
            this.profileService = profileService;
            this.authValidationService = authValidationService;
            this.emailSender = emailSender;

            Configuration = configuration;
        }

        public async Task<GenerateChangeEmailTokenResponse> Handle(GenerateChangeEmailTokenRequest request, CancellationToken cancellationToken)
        {
            if (await authValidationService.EmailExists(request.NewEmail))
                throw new DuplicateException("Email already exists", ErrorCodes.EmailExists);

            var generateChangeEmailTokenResult = await profileService.GenerateChangeEmailToken(request.NewEmail) ??
                throw new TokenException("Error occurred during generating change email token", ErrorCodes.CannotGenerateToken);

            return await emailSender.Send(EmailMessages.EmailChangeEmail(request.NewEmail,
                $"{Configuration.GetValue<string>(AppSettingsKeys.ClientAddress)}profile/changeEmail?userId={generateChangeEmailTokenResult.UserId}&token={generateChangeEmailTokenResult.Token}&newEmail={generateChangeEmailTokenResult.NewEmail}"))
                    ? new GenerateChangeEmailTokenResponse()
                    : throw new ServiceException("Sending change email callback failed", ErrorCodes.SendEmailFailed);
        }
    }
}