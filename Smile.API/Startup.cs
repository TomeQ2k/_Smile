using AspNetCoreRateLimit;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Smile.API.AppConfigs;
using Smile.Core.Common.Helpers;
using Smile.Infrastructure.Shared.SignalR;
using System.IO;
using System.Reflection;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Mapper;

namespace Smile.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.ConfigureAuthentication(Configuration);
            services.ConfigureAuthorization();

            services.ConfigureMvc();

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            services.AddOptions();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            services.ConfigureIpRateLimit(Configuration);

            services.ConfigureDbContext(Configuration);

            services.ConfigureCors();

            services.AddMediatR(Assembly.Load("Smile.Core.Application"));

            #region services

            services.ConfigureScopedServices();
            services.ConfigureSingletonServices();

            #endregion

            services.ConfigureSettings(Configuration);

            services.ConfigureServerHostedServices();

            services.AddAutoMapper(typeof(MapperProfile));

            services.AddDataProtection();

            services.AddDirectoryBrowser();

            services.AddSignalR();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, INLogger logger)
        {
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(Constants.CorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseIpRateLimiting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<HubClient>("/api/hub");
            });

            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, @"files")),
                RequestPath = new PathString("/files"),
                EnableDirectoryBrowsing = true
            });
        }
    }
}