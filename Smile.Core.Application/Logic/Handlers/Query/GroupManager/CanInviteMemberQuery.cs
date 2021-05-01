using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Query.GroupManager;
using Smile.Core.Application.Logic.Responses.Query.GroupManager;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Logic.Handlers.Query.GroupManager
{
    public class CanInviteMemberQuery : IRequestHandler<CanInviteMemberRequest, CanInviteMemberResponse>
    {
        private readonly IReadOnlyGroupManager groupManager;

        public CanInviteMemberQuery(IReadOnlyGroupManager groupManager)
        {
            this.groupManager = groupManager;
        }

        public async Task<CanInviteMemberResponse> Handle(CanInviteMemberRequest request, CancellationToken cancellationToken)
        {
            var canInviteMemberResult = await groupManager.CanInviteMember(request.Username, request.GroupId);

            return canInviteMemberResult != null ? new CanInviteMemberResponse { CanInvite = canInviteMemberResult.CanInvite, UserId = canInviteMemberResult.UserId }
                : throw new CrudException("Checking if user can be invited to group failed");
        }
    }
}