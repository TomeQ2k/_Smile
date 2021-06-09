using System.Threading.Tasks;
using Smile.Core.Application.Features.Requests.Query.Params;
using Smile.Core.Application.Models.Pagination;
using Smile.Core.Domain.Entities.LogEntity;

namespace Smile.Core.Application.Logging
{
    public interface IReadOnlyLogManager
    {
        Task<PagedList<LogDocument>> GetLogs(LogFiltersParams filters);
    }
}