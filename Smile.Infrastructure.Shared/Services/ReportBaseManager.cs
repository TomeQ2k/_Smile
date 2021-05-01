using Smile.Core.Common.Enums;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Data;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Infrastructure.Shared.Services
{
    public class ReportBaseManager : IReportBaseManager
    {
        protected readonly IDatabase database;
        protected readonly IFilesManager filesManager;
        protected readonly IReadOnlyProfileService profileService;

        private readonly IReadOnlyRolesService rolesService;

        public ReportBaseManager(IDatabase database, IFilesManager filesManager, IReadOnlyProfileService profileService, IReadOnlyRolesService rolesService)
        {
            this.database = database;
            this.filesManager = filesManager;
            this.profileService = profileService;
            this.rolesService = rolesService;
        }

        protected ReportBaseManager(IDatabase database, IFilesManager filesManager, IReadOnlyProfileService profileService)
        {
            this.database = database;
            this.filesManager = filesManager;
            this.profileService = profileService;
        }

        public async Task<ToggleReportStatusResult> ToggleReportStatus(string reportId)
        {
            if (!rolesService.IsPermitted(await profileService.GetCurrentUser(), Constants.SupportRoles))
                throw new NoPermissionsException("You are not permitted to toggle this report status");

            var report = await GetReport(reportId);

            if (report.ReporterId == null)
                return null;

            report.ToggleStatus();

            return await database.Complete() ? new ToggleReportStatusResult(report.IsClosed) : null;
        }

        public async Task<bool> DeleteReport(string reportId)
        {
            if (!rolesService.IsPermitted(await profileService.GetCurrentUser(), RoleName.HeadAdmin))
                throw new NoPermissionsException("You are not permitted to delete this report");

            var report = await GetReport(reportId);

            database.ReportRepository.Delete(report);

            if (report.ReportFiles.Any())
            {
                filesManager.DeleteDirectory($"files/reports/{report.Id}");
                filesManager.DeleteDirectory($"files/replies/{report.Id}");
            }

            return await database.Complete();
        }

        #region protected

        protected async Task<Report> GetReport(string reportId) => await database.ReportRepository.Get(reportId)
            ?? throw new EntityNotFoundException("Report not found");

        #endregion
    }
}