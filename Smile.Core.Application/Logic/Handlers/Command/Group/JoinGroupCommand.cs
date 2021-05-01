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
    public class JoinGroupCommand : IRequestHandler<JoinGroupRequest, JoinGroupResponse>
    {
        private readonly IGroupService groupService;
        private readonly IMapper mapper;

        public JoinGroupCommand(IGroupService groupService, IMapper mapper)
        {
            this.groupService = groupService;
            this.mapper = mapper;
        }

        public async Task<JoinGroupResponse> Handle(JoinGroupRequest request, CancellationToken cancellationToken)
        {
            var newMember = await groupService.JoinGroup(request.GroupId, request.JoinCode) ?? throw new CrudException("Member joining failed");

            return new JoinGroupResponse { Member = mapper.Map<GroupMemberDto>(newMember) };
        }
    }
}