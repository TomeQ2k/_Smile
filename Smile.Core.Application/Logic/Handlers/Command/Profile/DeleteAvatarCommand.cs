using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.Profile;
using Smile.Core.Application.Logic.Responses.Command.Profile;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.Profile
{
    public class DeleteAvatarCommand : IRequestHandler<DeleteAvatarRequest, DeleteAvatarResponse>
    {
        private readonly IProfileService profileService;

        public DeleteAvatarCommand(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public async Task<DeleteAvatarResponse> Handle(DeleteAvatarRequest request, CancellationToken cancellationToken)
            => await profileService.DeleteAvatar() ? new DeleteAvatarResponse()
                : throw new DeleteFileException("Error occurred during deleting user avatar");
    }
}