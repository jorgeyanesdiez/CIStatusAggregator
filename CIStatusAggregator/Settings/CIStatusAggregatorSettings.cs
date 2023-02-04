using System.Collections.Generic;

namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Settings for the application.
    /// </summary>
    public class CIStatusAggregatorSettings
    {
        public List<EndpointSettings> Endpoints { get; set; } = new List<EndpointSettings>();
    }

}
