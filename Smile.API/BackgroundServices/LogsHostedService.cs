using Microsoft.Extensions.DependencyInjection;
using Smile.Core.Common.Helpers;
using System;
using Smile.Core.Application.Logging;

namespace Smile.API.BackgroundServices
{
    internal class LogsHostedService : ServerHostedService
    {
        public LogsHostedService(INLogger logger, IServiceProvider services) : base(logger, services)
        {
            TimeInterval = Constants.ServerHostedServiceTimeInMinutes;
        }

        public override async void Callback(object state)
        {
            using (var scope = services.CreateScope())
            {
                var logManager = scope.ServiceProvider.GetRequiredService<ILogManager>();

                await logManager.StoreLogs();
                await logManager.ClearLogs();

                base.Callback(state);
            }
        }
    }
}