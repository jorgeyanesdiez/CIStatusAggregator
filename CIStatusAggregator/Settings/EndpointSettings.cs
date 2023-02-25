namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Settings for an endpoint.
    /// </summary>
    public record EndpointSettings
    {
        public required EndpointMetaSettings Meta { get; init; }
        public required EndpointRemoteSettings Remote { get; init; }
        public required EndpointLocalSettings Local { get; init; }
    }

}
