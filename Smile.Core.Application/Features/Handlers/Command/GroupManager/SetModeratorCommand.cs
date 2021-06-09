using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.GroupManager;
using Smile.Core.Application.Features.Responses.Command.GroupManager;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.GroupManager
{
    public class SetModeratorCommand : IRequestHandler<SetModeratorRequest, SetModeratorResponse>
    {
        private readonly IGroupManager groupManager;

        public SetModeratorCommand(IGroupManager groupManager)
        {
            this.groupManager = groupManager;
        }

        public async Task<SetModeratorResponse> Handle(SetModeratorRequest request, CancellationToken cancellationToken)
            => await groupManager.SetModerator(request.GroupId, request.UserId, request.IsModerator) ? new SetModeratorResponse { IsModerator = request.IsModerator }
                : throw new CrudException("Moderator status has not been changed");
    }
}