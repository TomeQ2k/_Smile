using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Query.Group;
using Smile.Core.Application.Features.Responses.Query.Group;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Features.Handlers.Query.Group
{
    public class GetGroupQuery : IRequestHandler<GetGroupRequest, GetGroupResponse>
    {
        private readonly IReadOnlyGroupService groupService;
        private readonly IMapper mapper;
        private readonly IHttpContextReader httpContextReader;

        public GetGroupQuery(IReadOnlyGroupService groupService, IMapper mapper, IHttpContextReader httpContextReader)
        {
            this.groupService = groupService;
            this.mapper = mapper;
            this.httpContextReader = httpContextReader;
        }

        public async Task<GetGroupResponse> Handle(GetGroupRequest request, CancellationToken cancellationToken)
        {
            string currentUserId = httpContextReader.CurrentUserId;

            var group = await groupService.GetGroup(request.GroupId) ??
                        throw new EntityNotFoundException("Group not found");

            group.SortPosts();
            group.SortMembers();

            var groupToReturn = mapper.Map<GroupDto>(group);

            if (currentUserId != groupToReturn.AdminId)
                groupToReturn.JoinCode = null;

            foreach (var member in groupToReturn.Members)
                member.IsJoining = !member.IsAccepted &&
                                   group.GroupInvites.Any(i => i.UserId == member.UserId && i.IsJoining);

            groupToReturn.IsMember =
                group.AdminId == currentUserId || group.GroupMembers.Any(m => m.UserId == currentUserId);

            groupToReturn.Members = currentUserId != group.AdminId
                ? groupToReturn.Members.Where(m => m.IsAccepted).ToList()
                : groupToReturn.Members;

            return new GetGroupResponse {Group = groupToReturn};
        }
    }
}