using Microsoft.Extensions.DependencyInjection;
using Smile.Core.Common.Helpers;
using System;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Services;

namespace Smile.API.BackgroundServices
{
    internal class DailyHostedService : ServerHostedService
    {
        public DailyHostedService(INLogger logger, IServiceProvider services) : base(logger, services)
        {
            TimeInterval = Constants.ServerHostedServiceTimeInMinutes;
        }

        public override async void Callback(object state)
        {
            using (var scope = services.CreateScope())
            {
                var storyManager = scope.ServiceProvider.GetRequiredService<IStoryManager>();
                var tokenManager = scope.ServiceProvider.GetRequiredService<ITokenManager>();

                await storyManager.ClearStories();
                await tokenManager.ClearExpiredTokens();

                base.Callback(state);
            }
        }
    }
}