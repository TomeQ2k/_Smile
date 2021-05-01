using Smile.Core.Domain.Data;
using System.Threading.Tasks;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Infrastructure.Shared.Services
{
    public class ReportAnonymousManager : ReportBaseManager, IReportAnonymousManager
    {
        public ReportAnonymousManager(IDatabase database, IFilesManager filesManager, IReadOnlyProfileService profileService)
            : base(database, filesManager, profileService) { }

        public async Task<Report> CreateAnonymousReport(string subject, string content, string email)
        {
            var anonymousReport = Report.Create(subject, content, email);

            database.ReportRepository.Add(anonymousReport);

            return await database.Complete() ? anonymousReport : null;
        }
    }
}