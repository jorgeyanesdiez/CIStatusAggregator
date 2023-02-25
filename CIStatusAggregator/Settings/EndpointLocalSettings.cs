namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Settings for the local part of an endpoint.
    /// </summary>
    public record EndpointLocalSettings
    {
        public required string StatusFilePath { get; init; }
    }

}
