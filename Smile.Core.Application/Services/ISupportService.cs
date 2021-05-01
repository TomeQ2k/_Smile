using System.Threading.Tasks;
using Smile.Core.Application.Logic.Requests.Query.Support;
using Smile.Core.Application.Models.Pagination;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Application.Services
{
    public interface ISupportService
    {
        Task<Report> GetReport(string reportId, string userId);

        Task<PagedList<Report>> FetchReports(FetchReportsPaginationRequest paginationRequest);
        Task<PagedList<Report>> FetchAllReports(FetchAllReportsPaginationRequest paginationRequest);
    }
}