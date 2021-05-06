using System.Threading.Tasks;
using Smile.Core.Application.Logic.Requests.Query.Support;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Application.Services
{
    public interface ISupportService
    {
        Task<Report> GetReport(string reportId, string userId);

        Task<IPagedList<Report>> FetchReports(FetchUserReportsPaginationRequest paginationRequest);
        Task<IPagedList<Report>> FetchAllReports(FetchAllReportsPaginationRequest paginationRequest);
    }
}