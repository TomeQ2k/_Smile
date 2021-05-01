using Smile.Core.Domain.Data.Repositories;
using System.Linq;
using Smile.Core.Application.SmartEnums;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        public ReportRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Report> GetFilteredReports(IReportFiltersParams filters)
        {
            var reports = context.Reports.AsQueryable();

            reports = ReportStatusSmartEnum.FromValue((int) filters.ReportStatus).Filter(reports);

            reports = ReportSortTypeSmartEnum.FromValue((int) filters.SortType).Sort(reports);

            return reports;
        }

        public IQueryable<Report> GetFilteredReportsWithReporterName(string reporterName, IReportFiltersParams filters)
        {
            var reports = context.Reports.AsQueryable();

            if (!string.IsNullOrEmpty(reporterName))
                reports = reports.Where(r => r.ReporterId == null
                    ? r.Email.ToLower().Contains(reporterName.ToLower())
                    : r.Reporter.Username.ToLower().Contains(reporterName.ToLower()));

            reports = ReportStatusSmartEnum.FromValue((int) filters.ReportStatus).Filter(reports);

            reports = ReportSortTypeSmartEnum.FromValue((int) filters.SortType).Sort(reports);

            return reports;
        }
    }
}