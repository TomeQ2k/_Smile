using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.GroupManager;
using Smile.Core.Application.Logic.Responses.Command.GroupManager;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.GroupManager
{
    public class DeleteGroupCommand : IRequestHandler<DeleteGroupRequest, DeleteGroupResponse>
    {
        private readonly IGroupManager groupManager;

        public DeleteGroupCommand(IGroupManager groupManager)
        {
            this.groupManager = groupManager;
        }

        public async Task<DeleteGroupResponse> Handle(DeleteGroupRequest request, CancellationToken cancellationToken)
            => await groupManager.DeleteGroup(request.GroupId) ? new DeleteGroupResponse()
                : throw new CrudException("Deleting group failed");
    }
}