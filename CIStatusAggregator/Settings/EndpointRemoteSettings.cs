namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Settings for the remote part of an endpoint.
    /// </summary>
    public record EndpointRemoteSettings
    {
        public required string BaseUrl { get; init; }
        public required JobNameFilterSettings? JobNameFilter { get; init; }
    }

}
