using Smile.Core.Common.Enums;
using Smile.Core.Domain.Data.Repositories.Params;

namespace Smile.Core.Application.Features.Requests.Query.Params
{
    public abstract class GroupFiltersParams : PaginationRequest, IGroupFiltersParams
    {
        public string Name { get; set; }
        public GroupAccessStatus AccessStatus { get; set; } = GroupAccessStatus.All;
        public GroupJoinStatus JoinStatus { get; set; } = GroupJoinStatus.All;
        public GroupSortType SortType { get; set; } = GroupSortType.CreatedDescending;
        public bool IsInvited { get; set; }

        public string UserId { get; set; }
    }
}