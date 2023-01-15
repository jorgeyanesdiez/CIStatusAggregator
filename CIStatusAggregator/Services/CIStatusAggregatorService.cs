using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CIStatusAggregator.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CIStatusAggregator.Services
{

    /// <summary>
    /// Orchestrates application services.
    /// </summary>
    public class CIStatusAggregatorService
        : BackgroundService
    {

        /// <summary>
        /// Allows integration with the container's lifetime.
        /// </summary>
        private IHostApplicationLifetime AppLifetime { get; }


        /// <summary>
        /// A logger for this service.
        /// </summary>
        private ILogger Logger { get; }


        /// <summary>
        /// The endpoint units to process.
        /// </summary>
        private IEnumerable<CIStatusAggregatorUnit> Units { get; }


        /// <summary>
        /// Main constructor.
        /// </summary>
        /// <param name="appLifetime">The value for the <see cref="AppLifetime"/> property.</param>
        /// <param name="logger">The value for the <see cref="Logger"/> property.</param>
        /// <param name="units">The value for the <see cref="Units"/> property.</param>
        public CIStatusAggregatorService(
            IHostApplicationLifetime appLifetime,
            ILogger<CIStatusAggregatorService> logger,
            IEnumerable<CIStatusAggregatorUnit> units
        )
        {
            AppLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Units = units ?? throw new ArgumentNullException(nameof(units));
            if (!units.Any()) { throw new ArgumentOutOfRangeException(nameof(units)); }
        }


        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken _)
        {
            var tasks = Task.WhenAll(Units.Select(async unit =>
            {
                Logger.LogInformation("Processing unit [{UnitDescription}].", unit.Description);
                var status = await unit.RemoteProcessor.GetStatus();
                unit.LocalProcessor.Serialize(status);
                Logger.LogInformation("Unit [{UnitDescription}] processed successfully.", unit.Description);
            }));

            try
            {
                await tasks;
            }
            catch (Exception exc)
            {
                var e = tasks.Exception?.Flatten() ?? exc;
                Logger.LogError("Error: {ExceptionMessage}", e.Message);
                throw e;
            }
            finally
            {
                AppLifetime.StopApplication();
            }
        }

    }

}
