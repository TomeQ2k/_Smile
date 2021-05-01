using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Smile.Core.Common.Helpers;
using System.Text;

namespace Smile.API.AppConfigs
{
    public static class AuthenticationAppConfig
    {
        public static AuthenticationBuilder ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
            => services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(configuration.GetSection(AppSettingsKeys.Token).Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }
}