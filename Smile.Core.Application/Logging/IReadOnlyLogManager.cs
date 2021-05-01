using System.Threading.Tasks;
using Smile.Core.Application.Logic.Requests.Query.LogRequests;
using Smile.Core.Application.Models.Mongo;
using Smile.Core.Application.Models.Pagination;

namespace Smile.Core.Application.Logging
{
    public interface IReadOnlyLogManager
    {
        Task<PagedList<LogDocument>> GetLogs(GetLogsPaginationRequest paginationRequest);
    }
}