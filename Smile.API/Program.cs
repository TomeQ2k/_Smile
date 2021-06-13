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
using System.Threading.Tasks;

namespace Smile.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var logger = NLogBuilder.ConfigureNLog(Constants.NlogConfig).GetCurrentClassLogger();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    logger.Info("Application started...");

                    var dataContext = services.GetRequiredService<DataContext>();
                    var databaseManager = services.GetRequiredService<IDatabaseManager>();

                    await dataContext.Database.MigrateAsync();
                    logger.Info("Database migration completed");

                    await databaseManager.Seed();

                    logger.Info("Application initialized");

                    host.Run();
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex.Message, "Application terminated unexpectedly...");
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
