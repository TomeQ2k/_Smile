using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Smile.Core.Application.Filters;
using Smile.Core.Application.Validators;

namespace Smile.API.AppConfigs
{
    public static class MvcAppConfig
    {
        public static IMvcBuilder ConfigureMvc(this IServiceCollection services)
            => services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
                options.Filters.Add(typeof(ExceptionFilter));
                options.Filters.Add(typeof(ValidatorBehavior));
                options.Filters.Add(typeof(BlockFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .AddMvcOptions(options => options.EnableEndpointRouting = false);
    }
}