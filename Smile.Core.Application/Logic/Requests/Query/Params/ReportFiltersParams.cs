using Smile.Core.Common.Enums;
using Smile.Core.Domain.Data.Repositories.Params;

namespace Smile.Core.Application.Logic.Requests.Query.Params
{
    public abstract class ReportFiltersParams : PaginationRequest, IReportFiltersParams
    {
        public ReportStatus ReportStatus { get; set; } = ReportStatus.All;
        public SortType SortType { get; set; } = SortType.Descending;
    }
}