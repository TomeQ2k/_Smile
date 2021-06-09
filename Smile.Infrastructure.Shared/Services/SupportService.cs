using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Data;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Query.Support;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Support;
using Smile.Core.Domain.Data.Models;

namespace Smile.Infrastructure.Shared.Services
{
    public class SupportService : ISupportService
    {
        private readonly IDatabase database;
        private readonly IReadOnlyRolesService rolesService;

        public SupportService(IDatabase database, IReadOnlyRolesService rolesService)
        {
            this.database = database;
            this.rolesService = rolesService;
        }

        public async Task<Report> GetReport(string reportId, string userId)
        {
            var report = (await database.ReportRepository.Find(r => r.Id == reportId))?.SortReplies()
                         ?? throw new EntityNotFoundException("Report not found");

            if (report.ReporterId != userId && !await rolesService.IsPermitted(userId, Constants.SupportRoles))
                throw new NoPermissionsException("This is not your report");

            return report;
        }

        public async Task<IPagedList<Report>> FetchReports(FetchUserReportsPaginationRequest paginationRequest)
            => await database.ReportRepository.GetFilteredUserReports(paginationRequest.UserId, paginationRequest, (paginationRequest.PageNumber, paginationRequest.PageSize));

        public async Task<IPagedList<Report>> FetchAllReports(FetchAllReportsPaginationRequest paginationRequest)
        {
            if (!await rolesService.IsPermitted(paginationRequest.CurrentUserId, Constants.SupportRoles))
                throw new NoPermissionsException("You are not allowed to perform this action");

            return await database.ReportRepository.GetFilteredReportsWithReporterName(paginationRequest.ReporterName, paginationRequest.ReportType,
                    paginationRequest, (paginationRequest.PageNumber, paginationRequest.PageSize));
        }
    }
}