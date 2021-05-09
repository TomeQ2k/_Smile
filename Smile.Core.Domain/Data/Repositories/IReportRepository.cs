using System.Threading.Tasks;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IReportRepository : IRepository<Report>
    {
        Task<IPagedList<Report>> GetFilteredUserReports(string userId, IReportFiltersParams filters, (int PageNumber, int PageSize) pagination);
        Task<IPagedList<Report>> GetFilteredReportsWithReporterName(string reporterName, ReportType reportType, IReportFiltersParams filters, (int PageNumber, int PageSize) pagination);
    }
}