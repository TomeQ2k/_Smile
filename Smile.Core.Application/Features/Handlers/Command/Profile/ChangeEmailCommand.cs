using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Profile;
using Smile.Core.Application.Features.Responses.Command.Profile;
using Smile.Core.Application.Services;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Handlers.Command.Profile
{
    public class ChangeEmailCommand : IRequestHandler<ChangeEmailRequest, ChangeEmailResponse>
    {
        private readonly IProfileService profileService;
        private readonly IAuthValidationService authValidationService;

        public ChangeEmailCommand(IProfileService profileService, IAuthValidationService authValidationService)
        {
            this.profileService = profileService;
            this.authValidationService = authValidationService;
        }

        public async Task<ChangeEmailResponse> Handle(ChangeEmailRequest request, CancellationToken cancellationToken)
        {
            if (await authValidationService.EmailExists(request.NewEmail))
                throw new DuplicateException("Email already exists", ErrorCodes.EmailExists);

            return await profileService.ChangeEmail(request.UserId, request.NewEmail, request.Token)
                ? new ChangeEmailResponse()
                : throw new ProfileUpdateException("Changing email failed");
        }
    }
}