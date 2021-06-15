using System.Threading.Tasks;
using Smile.Core.Application.Features.Requests.Query.Params;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Entities.LogEntity;

namespace Smile.Core.Application.Logging
{
    public interface IReadOnlyLogManager
    {
        Task<IPagedList<LogDocument>> GetLogs(LogFiltersParams filters);
    }
}