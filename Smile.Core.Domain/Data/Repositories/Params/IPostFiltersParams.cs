using Smile.Core.Common.Enums;

namespace Smile.Core.Domain.Data.Repositories.Params
{
    public interface IPostFiltersParams
    {
        string UserId { get; set; }
        string Title { get; set; }
        string GroupId { get; set; }

        PostSortType SortType { get; set; }
    }
}