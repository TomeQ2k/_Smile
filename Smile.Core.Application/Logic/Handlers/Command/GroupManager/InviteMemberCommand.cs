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
    public class InviteMemberCommand : IRequestHandler<InviteMemberRequest, InviteMemberResponse>
    {
        private readonly IGroupManager groupManager;
        private readonly IMapper mapper;

        public InviteMemberCommand(IGroupManager groupManager, IMapper mapper)
        {
            this.groupManager = groupManager;
            this.mapper = mapper;
        }

        public async Task<InviteMemberResponse> Handle(InviteMemberRequest request, CancellationToken cancellationToken)
        {
            var invitedMemberResult = await groupManager.InviteMember(request.GroupId, request.UserId);

            return invitedMemberResult != null ? new InviteMemberResponse
            { Member = mapper.Map<GroupMemberDto>(invitedMemberResult.Member), Invite = mapper.Map<GroupInviteDto>(invitedMemberResult.Invite) }
                : throw new CrudException("Invite member failed");
        }
    }
}