using Smile.Core.Common.Enums;
using Smile.Core.Domain.Data.Repositories.Params;

namespace Smile.Core.Application.Features.Requests.Query.Params
{
    public abstract class UserFiltersParams : PaginationRequest, IUserFiltersParams
    {
        public string Username { get; set; }

        public SortType SortType { get; set; } = SortType.Descending;
        public bool OnlyAdmin { get; set; }

        public EmailConfirmedStatus EmailConfirmedStatus { get; set; } = EmailConfirmedStatus.All;
        public BlockStatus BlockStatus { get; set; } = BlockStatus.All;

        public string UserId { get; set; }
    }
}