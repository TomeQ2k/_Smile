using System.Collections.Generic;
using System.Threading.Tasks;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Entities.LogEntity;
using Smile.Core.Domain.Mongo.Repositories.Params;

namespace Smile.Core.Domain.Mongo.Repositories
{
    public interface ILogMongoRepository : IMongoRepository<LogDocument>
    {
        Task<IPagedList<LogDocument>> GetFilteredLogs(ILogFiltersParams filters, (int PageNumber, int PageSize) pagination);
        Task<IEnumerable<LogDocument>> GetLogsToDelete();
    }
}