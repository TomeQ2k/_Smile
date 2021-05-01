using System.Threading.Tasks;
using Smile.Core.Application.Results;

namespace Smile.Core.Application.Services
{
    public interface IReportBaseManager
    {
        Task<ToggleReportStatusResult> ToggleReportStatus(string reportId);

        Task<bool> DeleteReport(string reportId);
    }
}