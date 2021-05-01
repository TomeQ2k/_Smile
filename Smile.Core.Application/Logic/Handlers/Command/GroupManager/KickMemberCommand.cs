using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.GroupManager;
using Smile.Core.Application.Logic.Responses.Command.GroupManager;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.GroupManager
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