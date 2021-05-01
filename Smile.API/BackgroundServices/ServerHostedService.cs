using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Smile.Core.Application.Logging;

namespace Smile.API.BackgroundServices
{
    internal abstract class ServerHostedService : IHostedService, IDisposable
    {
        protected readonly INLogger logger;
        protected readonly IServiceProvider services;

        public int TimeInterval { get; protected set; }

        private Timer timer;

        public ServerHostedService(INLogger logger, IServiceProvider services)
        {
            this.logger = logger;
            this.services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.Info($"{this.GetType().Name}: Background server hosted service started...");

            timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromMinutes(TimeInterval));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.Info($"{this.GetType().Name}: Background server hosted service stopped...");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public virtual void Callback(object state)
        {
            logger.Info($"{this.GetType().Name}: Background server hosted service invoked");
        }
    }
}