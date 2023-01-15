using CIStatusAggregator.Models;

namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Settings for the remote part of an endpoint.
    /// </summary>
    public class EndpointRemoteSettings
    {
        public string BaseUrl { get; set; }
        public string JobNameFilterRegex { get; set; }
        public RegexFilterMode JobNameFilterMode { get; set; }
    }

}
