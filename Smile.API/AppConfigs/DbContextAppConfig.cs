using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smile.Core.Common.Helpers;
using Smile.Infrastructure.Persistence.Database;

namespace Smile.API.AppConfigs
{
    public static class DbContextAppConfig
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<DataContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseMySql(configuration.GetConnectionString(AppSettingsKeys.ConnectionString), b => b.MigrationsAssembly("Smile.API"));
            });
    }
}