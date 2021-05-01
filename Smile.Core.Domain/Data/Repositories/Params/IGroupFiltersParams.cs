using Smile.Core.Common.Enums;

namespace Smile.Core.Domain.Data.Repositories.Params
{
    public interface IGroupFiltersParams
    {
        string Name { get; set; }
        GroupAccessStatus AccessStatus { get; set; }
        GroupJoinStatus JoinStatus { get; set; }
        GroupSortType SortType { get; set; }
        bool IsInvited { get; set; }

        string UserId { get; set; }
    }
}