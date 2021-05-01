using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Smile.Core.Application.Settings;

namespace Smile.API.AppConfigs
{
    public static class SettingsAppConfig
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<MongoDatabaseSettings>(configuration.GetSection(nameof(MongoDatabaseSettings)));

            services.AddSingleton<IMongoDatabaseSettings>(settings => settings.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

            return services;
        }
    }
}