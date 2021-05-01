using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.Group;
using Smile.Core.Application.Logic.Responses.Command.Group;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.Group
{
    public class CreateGroupCommand : IRequestHandler<CreateGroupRequest, CreateGroupResponse>
    {
        private readonly IGroupService groupService;
        private readonly IMapper mapper;

        public CreateGroupCommand(IGroupService groupService, IMapper mapper)
        {
            this.groupService = groupService;
            this.mapper = mapper;
        }

        public async Task<CreateGroupResponse> Handle(CreateGroupRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsPrivate)
                request.JoinCode = null;

            var createdGroup = await groupService.CreateGroup(request.Name, request.Description, request.IsPrivate, request.Image, request.JoinCode, request.InviteMemberPermission, request.RemoveMemberPermission);

            return createdGroup != null ? new CreateGroupResponse { Group = mapper.Map<GroupDto>(createdGroup) }
                : throw new CrudException("Group has not been created");
        }
    }
}