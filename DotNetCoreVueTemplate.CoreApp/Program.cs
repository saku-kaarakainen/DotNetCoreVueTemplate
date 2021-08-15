using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreVueTemplate.CoreApp
{
    public class Program
    {
        public static void Main(string[] args) => 
            CreateHostBuilder(args).Build()
            .CreateDatabaseIfNotExists().Run();
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.AddFilter((provider, category, logLevel) =>
                {
                    if (provider.Contains("ConsoleLoggerProvider")
                        && category.Contains("Controller")
                        && logLevel >= LogLevel.Information)
                    {
                        return true;
                    }
                    else if (provider.Contains("ConsoleLoggerProvider")
                        && category.Contains("Microsoft")
                        && logLevel >= LogLevel.Information)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public static class IHostExtensions
    {
        public static IHost CreateDatabaseIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<Database.DotnetCoreVueTemplateContext>();
                context.CreateDatabaseIfNotExists();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }

            return host;
        }
    }
}
