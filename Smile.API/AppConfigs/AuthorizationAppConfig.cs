using Microsoft.Extensions.DependencyInjection;
using Smile.Core.Common.Helpers;

namespace Smile.API.AppConfigs
{
    public static class AuthorizationAppConfig
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
            => services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.AdminPolicy, policy => policy.RequireRole(Constants.AdminRoles));
                options.AddPolicy(Constants.HeadAdminPolicy, policy => policy.RequireRole(Constants.HeadAdminRole));
            });
    }
}