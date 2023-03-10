using CIStatusAggregator.Commons.Factories;
using CIStatusAggregator.Commons.Services;
using CIStatusAggregator.Models;
using CIStatusAggregator.Services;
using CIStatusAggregator.Settings;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CIStatusAggregator
{

    /// <summary>
    /// The program's entry point.
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Entry point from the command line.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }


        /// <summary>
        /// Creates and configures a host builder for later use.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The requested host builder.</returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Environment.CurrentDirectory);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json");
                    configApp.AddUserSecrets<Program>(optional: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var serializerSettings = NewtonsoftJsonSerializerSettingsFactory.Build();
                    FlurlHttp.Configure(s => s.JsonSerializer = new NewtonsoftJsonSerializer(serializerSettings));

                    var appSettings = hostContext.Configuration.Get<AppSettings>()?.CIStatusAggregator
                        ?? throw new InvalidOperationException("The application settings could not be read");

                    appSettings.Endpoints.ForEach(endpoint => services.AddSingleton(sp => new CIStatusAggregatorItem()
                    {
                        Description = endpoint.Meta.Description,
                        RemoteProcessor = new JenkinsStatusProvider(endpoint.Remote),
                        LocalProcessor = new NewtonsoftJsonFileSerializer<CIStatus>(endpoint.Local.StatusFilePath, serializerSettings)
                    }));

                    services.AddHostedService<CIStatusAggregatorService>();
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                })
                .UseConsoleLifetime();
        }

    }

}
