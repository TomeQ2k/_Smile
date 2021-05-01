using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.Profile;
using Smile.Core.Application.Logic.Responses.Command.Profile;
using Smile.Core.Application.Services;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Logic.Handlers.Command.Profile
{
    public class ChangeUsernameCommand : IRequestHandler<ChangeUsernameRequest, ChangeUsernameResponse>
    {
        private readonly IProfileService profileService;
        private readonly IAuthValidationService authValidationService;

        public ChangeUsernameCommand(IProfileService profileService, IAuthValidationService authValidationService)
        {
            this.profileService = profileService;
            this.authValidationService = authValidationService;
        }

        public async Task<ChangeUsernameResponse> Handle(ChangeUsernameRequest request, CancellationToken cancellationToken)
        {
            if (await authValidationService.UsernameExists(request.NewUsername))
                throw new DuplicateException("Username already exists", ErrorCodes.UsernameExists);

            return await profileService.ChangeUsername(request.NewUsername)
                ? new ChangeUsernameResponse()
                : throw new ProfileUpdateException("Username change failed");
        }
    }
}