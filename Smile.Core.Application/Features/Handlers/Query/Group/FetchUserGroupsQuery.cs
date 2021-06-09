using System.Collections.Generic;
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
using Smile.Core.Application.ServiceUtils;

namespace Smile.Core.Application.Features.Handlers.Query.Group
{
    public class FetchUserGroupsQuery : IRequestHandler<FetchUserGroupsRequest, FetchUserGroupsResponse>
    {
        private readonly IReadOnlyGroupService groupService;
        private readonly IMapper mapper;
        private readonly IHttpContextReader httpContextReader;

        public FetchUserGroupsQuery(IReadOnlyGroupService groupService, IMapper mapper,
            IHttpContextReader httpContextReader)
        {
            this.groupService = groupService;
            this.mapper = mapper;
            this.httpContextReader = httpContextReader;
        }

        public async Task<FetchUserGroupsResponse> Handle(FetchUserGroupsRequest request,
            CancellationToken cancellationToken)
        {
            string currentUserId = httpContextReader.CurrentUserId;

            var userGroupsResult = await groupService.FetchUserGroups() ??
                                   throw new CrudException("Fetching user groups failed");

            var ownGroupsToReturn = mapper.Map<List<GroupListDto>>(userGroupsResult.OwnGroups);
            var myGroupsToReturn = mapper.Map<List<GroupListDto>>(userGroupsResult.MyGroups);

            GroupUtils.SetGroupMemberParams(ownGroupsToReturn, userGroupsResult.OwnGroups, currentUserId);
            GroupUtils.SetGroupMemberParams(myGroupsToReturn, userGroupsResult.MyGroups, currentUserId);

            return new FetchUserGroupsResponse
            {
                OwnGroups = ownGroupsToReturn,
                MyGroups = myGroupsToReturn
            };
        }
    }
}