using System.Threading.Tasks;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Application.Services
{
    public interface IReportAnonymousManager
    {
        Task<Report> CreateAnonymousReport(string subject, string content, string email);
    }
}