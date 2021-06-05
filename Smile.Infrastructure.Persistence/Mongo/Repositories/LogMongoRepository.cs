using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Smile.Core.Application.Settings;
using Smile.Core.Application.SmartEnums;
using Smile.Core.Domain.Entities.LogEntity;
using Smile.Core.Domain.Mongo.Repositories;
using Smile.Core.Domain.Mongo.Repositories.Params;

namespace Smile.Infrastructure.Persistence.Mongo.Repositories
{
    public class LogMongoRepository : MongoRepository<LogDocument>, ILogMongoRepository
    {
        public LogMongoRepository(IMongoDatabaseSettings settings) : base(settings)
        {
        }

        public async Task<IEnumerable<LogDocument>> GetFilteredLogs(ILogFiltersParams filters)
        {
            var logs = collection.AsQueryable();

            logs = logs.Where(l => l.Date >= filters.MinDate
                                   && l.Date <= filters.MaxDate);

            if (!string.IsNullOrEmpty(filters.Level))
                logs = logs.Where(l => l.Level.ToLower() == filters.Level.ToLower());

            if (!string.IsNullOrEmpty(filters.Message))
                logs = logs.Where(l => l.Message.ToLower().Contains(filters.Message.ToLower()));

            if (!string.IsNullOrEmpty(filters.Url))
                logs = logs.Where(l => l.Url.ToLower().Contains(filters.Url.ToLower()));

            if (!string.IsNullOrEmpty(filters.Action))
                logs = logs.Where(l => l.Action.ToLower().Contains(filters.Action.ToLower()));

            logs = LogSortTypeSmartEnum.FromValue((int) filters.SortType).Sort(logs);

            return await logs.ToListAsync();
        }
    }
}