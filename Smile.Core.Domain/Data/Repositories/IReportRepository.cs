using System.Linq;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IReportRepository : IRepository<Report>
    {
        IQueryable<Report> GetFilteredReports(IReportFiltersParams filters);

        IQueryable<Report> GetFilteredReportsWithReporterName(string reporterName, IReportFiltersParams filters);
    }
}