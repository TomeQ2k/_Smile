using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Features.Requests.Command.GroupManager;
using Smile.Core.Application.Features.Responses.Command.GroupManager;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.GroupManager
{
    public class AcceptMemberCommand : IRequestHandler<AcceptMemberRequest, AcceptMemberResponse>
    {
        private readonly IGroupManager groupManager;
        private readonly IMapper mapper;

        public AcceptMemberCommand(IGroupManager groupManager, IMapper mapper)
        {
            this.groupManager = groupManager;
            this.mapper = mapper;
        }

        public async Task<AcceptMemberResponse> Handle(AcceptMemberRequest request, CancellationToken cancellationToken)
        {
            var member = await groupManager.AcceptMember(request.GroupId, request.UserId, request.Accept);

            return new AcceptMemberResponse { Member = request.Accept ? mapper.Map<GroupMemberDto>(member) : null };
        }
    }
}