using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.GroupManager;
using Smile.Core.Application.Logic.Responses.Command.GroupManager;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.GroupManager
{
    public class LeaveGroupCommand : IRequestHandler<LeaveGroupRequest, LeaveGroupResponse>
    {
        private readonly IGroupManager groupManager;

        public LeaveGroupCommand(IGroupManager groupManager)
        {
            this.groupManager = groupManager;
        }

        public async Task<LeaveGroupResponse> Handle(LeaveGroupRequest request, CancellationToken cancellationToken)
            => await groupManager.LeaveGroup(request.GroupId) ? new LeaveGroupResponse()
                : throw new CrudException("Leaving group failed");
    }
}