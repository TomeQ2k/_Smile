using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.GroupManager;
using Smile.Core.Application.Features.Responses.Command.GroupManager;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.GroupManager
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