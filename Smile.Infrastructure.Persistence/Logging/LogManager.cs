using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Builders;
using Smile.Core.Application.Features.Requests.Query.Params;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Services;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Entities.LogEntity;
using Smile.Core.Domain.Mongo.Repositories;

namespace Smile.Infrastructure.Persistence.Logging
{
    public class LogManager : ILogManager
    {
        private readonly ILogMongoRepository logMongoRepository;
        private readonly IFilesManager filesManager;

        public LogManager(ILogMongoRepository logMongoRepository, IFilesManager filesManager)
        {
            this.logMongoRepository = logMongoRepository;
            this.filesManager = filesManager;
        }

        public async Task<bool> StoreLogs()
        {
            string logsFilePath = $"/logs/api-logs-{DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")}.log";

            if (!filesManager.FileExists(logsFilePath))
                return false;

            var allLogs = await LoadLogsFromFile(logsFilePath);

            foreach (var log in from fileLog in allLogs
                let logProps = fileLog.Split("$|")
                let log = new LogBuilder()
                    .CreatedAt(DateTime.Parse(logProps[0]))
                    .SetLevel(logProps[1])
                    .SetLogger(logProps[2])
                    .SetMessage(logProps[3], logProps[4])
                    .WithAction(logProps[5], logProps[6])
                    .Build()
                select log)
                await logMongoRepository.Insert(log);

            filesManager.Delete(logsFilePath);

            return true;
        }

        public async Task ClearLogs()
        {
            var logsToDelete = (await logMongoRepository.GetLogsToDelete()).ToList();

            for (int i = 0; i < logsToDelete.Count; i++)
                await logMongoRepository.Delete(logsToDelete[i].Id);
        }

        public async Task<IPagedList<LogDocument>> GetLogs(LogFiltersParams filters)
            => await logMongoRepository.GetFilteredLogs(filters, (filters.PageNumber, filters.PageSize));

        #region private

        private async Task<IEnumerable<string>> LoadLogsFromFile(string logsFilePath)
            => (await filesManager.ReadFile(logsFilePath)).Replace("\r\n", "").Split("$#").Skip(1);

        #endregion
    }
}