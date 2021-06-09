using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.GroupManager;
using Smile.Core.Application.Features.Responses.Command.GroupManager;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.GroupManager
{
    public class KickMemberCommand : IRequestHandler<KickMemberRequest, KickMemberResponse>
    {
        private readonly IGroupManager groupManager;

        public KickMemberCommand(IGroupManager groupManager)
        {
            this.groupManager = groupManager;
        }

        public async Task<KickMemberResponse> Handle(KickMemberRequest request, CancellationToken cancellationToken)
            => await groupManager.KickMember(request.GroupId, request.UserId) ? new KickMemberResponse()
                : throw new CrudException("Kick member failed");
    }
}