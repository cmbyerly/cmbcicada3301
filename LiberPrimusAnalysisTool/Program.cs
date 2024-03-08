using LiberPrimusAnalysisTool.Database.DBRepos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LiberPrimusAnalysisTool
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            // create hosting object and DI layer
            using IHost host = CreateHostBuilder(args).Build();

            // create a service scope
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                services.GetRequiredService<App>().Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // implementatinon of 'CreateHostBuilder' static method and create host object
        public static IHostBuilder CreateHostBuilder(string[] strings)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((_, services) =>
                {
                    // Database services
                    services.AddSingleton<IDatabaseConnectionUtils, DatabaseConnectionUtils>();
                    services.AddSingleton<IPixelInfoData, PixelInfoData>();
                    services.AddSingleton<ILineOrientationData, LineOrientationData>();
                    services.AddSingleton<ILineColorInfoData, LineColorInfoData>();
                    services.AddSingleton<ILiberPageData, LiberPageData>();
                    services.AddSingleton<ILiberColorInfoData, LiberColorInfoData>();
                    services.AddSingleton<ILiberColorData, LiberColorData>();
                    services.AddSingleton<ILiberColorData, LiberColorData>();

                    // The application singleton
                    services.AddSingleton<App>();
                });
        }
    }
}