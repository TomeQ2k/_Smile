using Microsoft.Extensions.DependencyInjection;
using Smile.API.BackgroundServices;

namespace Smile.API.AppConfigs
{
    public static class ServerHostedServicesAppConfig
    {
        public static IServiceCollection ConfigureServerHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<DailyHostedService>();
            services.AddHostedService<LogsHostedService>();

            return services;
        }
    }
}