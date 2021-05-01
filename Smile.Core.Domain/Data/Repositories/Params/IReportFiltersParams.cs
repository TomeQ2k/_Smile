using Smile.Core.Common.Enums;

namespace Smile.Core.Domain.Data.Repositories.Params
{
    public interface IReportFiltersParams
    {
        ReportStatus ReportStatus { get; set; }
        SortType SortType { get; set; }
    }
}