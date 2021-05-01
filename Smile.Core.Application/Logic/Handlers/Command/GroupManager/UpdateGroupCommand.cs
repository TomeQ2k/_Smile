using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.GroupManager;
using Smile.Core.Application.Logic.Responses.Command.GroupManager;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.GroupManager
{
    public class UpdateGroupCommand : IRequestHandler<UpdateGroupRequest, UpdateGroupResponse>
    {
        private readonly IGroupManager groupManager;
        private readonly IMapper mapper;

        public UpdateGroupCommand(IGroupManager groupManager, IMapper mapper)
        {
            this.groupManager = groupManager;
            this.mapper = mapper;
        }

        public async Task<UpdateGroupResponse> Handle(UpdateGroupRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsPrivate)
                request.JoinCode = null;

            var updatedGroup = await groupManager.UpdateGroup(request.GroupId, request);

            return updatedGroup != null ? new UpdateGroupResponse { Group = mapper.Map<GroupDto>(updatedGroup) }
                : throw new CrudException("Group has not been updated");
        }
    }
}