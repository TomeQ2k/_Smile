using Microsoft.Extensions.DependencyInjection;
using Smile.Core.Common.Helpers;

namespace Smile.API.AppConfigs
{
    public static class CorsAppConfig
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
            => services.AddCors(options => options.AddPolicy(Constants.CorsPolicy, build =>
            {
                build.AllowCredentials()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithOrigins("http://localhost:4200");
            }));
    }
}