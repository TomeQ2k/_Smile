using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Profile;
using Smile.Core.Application.Features.Responses.Command.Profile;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Profile
{
    public class SetAvatarCommand : IRequestHandler<SetAvatarRequest, SetAvatarResponse>
    {
        private readonly IProfileService profileService;

        public SetAvatarCommand(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public async Task<SetAvatarResponse> Handle(SetAvatarRequest request, CancellationToken cancellationToken)
        {
            var avatarUrl = await profileService.SetAvatar(request.Avatar);

            return !string.IsNullOrEmpty(avatarUrl) ? new SetAvatarResponse { AvatarUrl = avatarUrl }
                : throw new UploadFileException("Error occurred during changing user avatar");
        }
    }
}