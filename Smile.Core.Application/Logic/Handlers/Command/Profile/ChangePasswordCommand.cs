using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.Profile;
using Smile.Core.Application.Logic.Responses.Command.Profile;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.Profile
{
    public class ChangePasswordCommand : IRequestHandler<ChangePasswordRequest, ChangePasswordResponse>
    {
        private readonly IProfileService profileService;

        public ChangePasswordCommand(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var changePasswordResult = await profileService.ChangePassword(request.OldPassword, request.NewPassword)
                ?? throw new ChangePasswordException("Error occurred during changing password");

            return changePasswordResult.HasChanged ? new ChangePasswordResponse()
                : throw new OldPasswordInvalidException(changePasswordResult.Message);
        }
    }
}