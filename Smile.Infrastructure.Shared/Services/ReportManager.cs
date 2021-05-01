using Microsoft.AspNetCore.Http;
using Smile.Core.Domain.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Infrastructure.Shared.Services
{
    public class ReportManager : ReportBaseManager, IReportManager
    {
        public ReportManager(IDatabase database, IFilesManager filesManager, IReadOnlyProfileService profileService)
            : base(database, filesManager, profileService) { }

        public async Task<Report> CreateReport(string subject, string content, IEnumerable<IFormFile> files = null)
        {
            var currentUser = await profileService.GetCurrentUser();

            var report = Report.Create(subject, content);

            currentUser.Reports.Add(report);

            if (!await database.Complete())
                return null;

            await AttachFiles(files, report);

            return report;
        }

        #region private

        private async Task AttachFiles(IEnumerable<IFormFile> files, Report report)
        {
            if (files != null)
            {
                var filesUploaded = await filesManager.Upload(files, $"reports/{report.Id}");

                filesUploaded.ToList().ForEach(f => report.ReportFiles.Add(ReportFile.Create<ReportFile>(f.Url, f.Path)));

                await database.Complete();
            }
        }

        #endregion
    }
}