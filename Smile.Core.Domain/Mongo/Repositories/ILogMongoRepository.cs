using System.Collections.Generic;
using System.Threading.Tasks;
using Smile.Core.Domain.Entities.LogEntity;
using Smile.Core.Domain.Mongo.Repositories.Params;

namespace Smile.Core.Domain.Mongo.Repositories
{
    public interface ILogMongoRepository : IMongoRepository<LogDocument>
    {
        Task<IEnumerable<LogDocument>> GetFilteredLogs(ILogFiltersParams filters);
        Task<IEnumerable<LogDocument>> GetLogsToDelete();
    }
}