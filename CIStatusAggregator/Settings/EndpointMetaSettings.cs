namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Settings for the meta part of an endpoint.
    /// </summary>
    public record EndpointMetaSettings
    {
        public required string Description { get; init; }
    }

}
