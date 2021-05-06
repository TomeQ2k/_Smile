using Smile.Core.Domain.Data.Repositories;
using System.Linq;
using Smile.Core.Application.SmartEnums;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Support;
using System.Threading.Tasks;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Application.Extensions;
using Smile.Core.Common.Enums;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        public ReportRepository(DataContext context) : base(context)
        {
        }

        public async Task<IPagedList<Report>> GetFilteredUserReports(string currentUserId, IReportFiltersParams filters, (int PageNumber, int PageSize) pagination)
        {
            var reports = context.Reports.Where(r => r.ReporterId == currentUserId);

            reports = ReportStatusSmartEnum.FromValue((int)filters.ReportStatus).Filter(reports);

            reports = ReportSortTypeSmartEnum.FromValue((int)filters.SortType).Sort(reports);

            return await reports.ToPagedList<Report>(pagination.PageNumber, pagination.PageSize);
        }

        public async Task<IPagedList<Report>> GetFilteredReportsWithReporterName(string reporterName, ReportType reportType, IReportFiltersParams filters, (int PageNumber, int PageSize) pagination)
        {
            var reports = context.Reports.AsQueryable();

            if (!string.IsNullOrEmpty(reporterName))
                reports = reports.Where(r => r.ReporterId == null
                    ? r.Email.ToLower().Contains(reporterName.ToLower())
                    : r.Reporter.Username.ToLower().Contains(reporterName.ToLower()));

            reports = ReportTypeSmartEnum.FromValue((int)reportType).Filter(reports);
            reports = ReportStatusSmartEnum.FromValue((int)filters.ReportStatus).Filter(reports);

            reports = ReportSortTypeSmartEnum.FromValue((int)filters.SortType).Sort(reports);

            return await reports.ToPagedList<Report>(pagination.PageNumber, pagination.PageSize);
        }
    }
}