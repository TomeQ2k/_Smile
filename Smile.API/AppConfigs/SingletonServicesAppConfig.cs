using Microsoft.Extensions.DependencyInjection;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Infrastructure.Persistence.Logging;
using Smile.Infrastructure.Shared.Services;

namespace Smile.API.AppConfigs
{
    public static class SingletonServicesAppConfig
    {
        public static IServiceCollection ConfigureSingletonServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IFilesManager, FilesManager>();
            services.AddSingleton<IHashGenerator, HashGenerator>();
            services.AddSingleton<INLogger, NLogger>();
            services.AddSingleton<IHttpContextService, HttpContextService>();
            services.AddSingleton<IHttpContextWriter, HttpContextService>();
            services.AddSingleton<IHttpContextReader, HttpContextService>();

            services.AddSingleton<IReadOnlyFilesManager, FilesManager>();

            return services;
        }
    }
}