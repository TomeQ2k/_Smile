using Smile.Core.Common.Enums;

namespace Smile.Core.Domain.Data.Repositories.Params
{
    public interface IUserFiltersParams
    {
        string Username { get; set; }

        SortType SortType { get; set; }
        bool OnlyAdmin { get; set; }

        EmailConfirmedStatus EmailConfirmedStatus { get; set; }
        BlockStatus BlockStatus { get; set; }

        string UserId { get; set; }
    }
}