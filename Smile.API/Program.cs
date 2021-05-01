using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Smile.API.BackgroundServices.Interfaces;
using Smile.Core.Common.Helpers;
using Smile.Infrastructure.Persistence.Database;
using System;

namespace Smile.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var logger = NLogBuilder.ConfigureNLog(Constants.NlogConfig).GetCurrentClassLogger();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dataContext = services.GetRequiredService<DataContext>();
                    var databaseManager = services.GetRequiredService<IDatabaseManager>();

                    dataContext.Database.Migrate();

                    databaseManager.Seed();

                    logger.Debug("Application initialized...");

                    host.Run();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message, "Program stopped because of exception");
                    throw;
                }
                finally
                {
                    NLog.LogManager.Shutdown();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
    }
}