using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Logic.Requests.Query.Group;
using Smile.Core.Application.Logic.Responses.Query.Group;
using Smile.Core.Application.Models.Pagination;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Application.ServiceUtils;

namespace Smile.Core.Application.Logic.Handlers.Query.Group
{
    public class FetchGroupsQuery : IRequestHandler<FetchGroupsPaginationRequest, FetchGroupsPaginationResponse>
    {
        private readonly IReadOnlyGroupService groupService;
        private readonly IMapper mapper;
        private readonly IHttpContextService httpContextService;

        public FetchGroupsQuery(IReadOnlyGroupService groupService, IMapper mapper,
            IHttpContextService httpContextService)
        {
            this.groupService = groupService;
            this.mapper = mapper;
            this.httpContextService = httpContextService;
        }

        public async Task<FetchGroupsPaginationResponse> Handle(FetchGroupsPaginationRequest request,
            CancellationToken cancellationToken)
        {
            string currentUserId = httpContextService.CurrentUserId;

            request.UserId = currentUserId;

            var groups = await groupService.FetchGroups(request);

            var groupsToReturn = mapper.Map<List<GroupListDto>>(groups);

            GroupUtils.SetGroupMemberParams(groupsToReturn, groups as PagedList<Domain.Entities.Group.Group>, currentUserId);

            httpContextService.AddPagination(groups.CurrentPage, groups.PageSize, groups.TotalCount, groups.TotalPages);

            return new FetchGroupsPaginationResponse { Groups = groupsToReturn };
        }
    }
}