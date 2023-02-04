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
        /// The endpoint items to process.
        /// </summary>
        private IEnumerable<CIStatusAggregatorItem> Items { get; }


        /// <summary>
        /// Main constructor.
        /// </summary>
        /// <param name="appLifetime">The value for the <see cref="AppLifetime"/> property.</param>
        /// <param name="logger">The value for the <see cref="Logger"/> property.</param>
        /// <param name="items">The value for the <see cref="Items"/> property.</param>
        public CIStatusAggregatorService(
            IHostApplicationLifetime appLifetime,
            ILogger<CIStatusAggregatorService> logger,
            IEnumerable<CIStatusAggregatorItem> items
        )
        {
            AppLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Items = items ?? throw new ArgumentNullException(nameof(items));
            if (!items.Any()) { throw new ArgumentOutOfRangeException(nameof(items)); }
        }


        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken _)
        {
            var processTask = ProcessItemsAsync();
            try { await processTask; }
            catch (Exception exc)
            {
                var e = processTask.Exception?.Flatten() ?? exc;
                Logger.LogError("Error: {ExceptionMessage}", e.Message);
                throw e;
            }
            finally { AppLifetime.StopApplication(); }
        }


        /// <summary>
        /// Orchestrates services to process each defined item.
        /// </summary>
        /// <returns>The task context.</returns>
        public Task ProcessItemsAsync()
        {
            return Task.WhenAll(Items.Select(async item =>
            {
                Logger.LogInformation("Processing item [{ItemDescription}].", item.Description);
                var status = await item.RemoteProcessor.GetStatus();
                item.LocalProcessor.Serialize(status);
                Logger.LogInformation("Item [{ItemDescription}] processed successfully.", item.Description);
            }));
        }

    }

}
