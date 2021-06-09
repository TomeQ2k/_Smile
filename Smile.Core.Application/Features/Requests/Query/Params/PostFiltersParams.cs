using Smile.Core.Common.Enums;
using Smile.Core.Domain.Data.Repositories.Params;

namespace Smile.Core.Application.Features.Requests.Query.Params
{
    public abstract class PostFiltersParams : PaginationRequest, IPostFiltersParams
    {
        public string Title { get; set; }
        public string UserId { get; set; }
        public string GroupId { get; set; }

        public PostSortType SortType { get; set; } = PostSortType.Newest;
    }
}