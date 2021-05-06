using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Builders;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Logic.Requests.Query.LogRequests;
using Smile.Core.Application.Models.Mongo;
using Smile.Core.Application.Models.Pagination;
using Smile.Core.Application.Services;
using Smile.Core.Application.SmartEnums;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Data.Mongo;

namespace Smile.Infrastructure.Persistence.Logging
{
    public class LogManager : ILogManager
    {
        private readonly IMongoRepository<LogDocument> logsMongoRepository;
        private readonly IFilesManager filesManager;

        public LogManager(IMongoRepository<LogDocument> logsMongoRepository, IFilesManager filesManager)
        {
            this.logsMongoRepository = logsMongoRepository;
            this.filesManager = filesManager;
        }

        public async Task<bool> StoreLogs()
        {
            string logsFileRelativePath = $"logs/api-logs-{DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")}.log";
            string logsFilePath = $@"{filesManager.WebRootPath}/{logsFileRelativePath}";

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
                await logsMongoRepository.Insert(log);

            filesManager.Delete(logsFileRelativePath);

            return true;
        }

        public async Task ClearLogs()
        {
            var logsToDelete = (await logsMongoRepository.GetAll())
                .Where(l => ((l.Level == Constants.INFO || l.Level == Constants.DEBUG) &&
                             l.Date.AddMonths(Constants.InfoLogLifeTimeInMonths) < DateTime.Now)
                            || (l.Level == Constants.WARNING &&
                                l.Date.AddMonths(Constants.WarningLogLifeTimeInMonths) < DateTime.Now)
                            || (l.Level == Constants.ERROR &&
                                l.Date.AddMonths(Constants.ErrorLogLifeTimeInMonths) < DateTime.Now))
                .ToList();

            for (int i = 0; i < logsToDelete.Count; i++)
                await logsMongoRepository.Delete(logsToDelete[i].Id.ToString());
        }

        public async Task<PagedList<LogDocument>> GetLogs(GetLogsPaginationRequest paginationRequest)
        {
            var logs = (await logsMongoRepository.GetAll()).Where(l => l.Date >= paginationRequest.MinDate
                                                                       && l.Date <= paginationRequest.MaxDate);

            if (!string.IsNullOrEmpty(paginationRequest.Level))
                logs = logs.Where(l => l.Level.ToLower() == paginationRequest.Level.ToLower());

            if (!string.IsNullOrEmpty(paginationRequest.Message))
                logs = logs.Where(l => l.Message.ToLower().Contains(paginationRequest.Message.ToLower()));

            if (!string.IsNullOrEmpty(paginationRequest.Url))
                logs = logs.Where(l => l.Url.ToLower().Contains(paginationRequest.Url.ToLower()));

            if (!string.IsNullOrEmpty(paginationRequest.Action))
                logs = logs.Where(l => l.Action.ToLower().Contains(paginationRequest.Action.ToLower()));

            logs = LogSortTypeSmartEnum.FromValue((int)paginationRequest.SortType).Sort(logs);

            return logs.ToPagedList<LogDocument>(paginationRequest.PageNumber, paginationRequest.PageSize);
        }

        #region private

        private async Task<IEnumerable<string>> LoadLogsFromFile(string logsFilePath)
            => (await filesManager.ReadFile(logsFilePath)).Replace("\r\n", "").Split("$#").Skip(1);

        #endregion
    }
}