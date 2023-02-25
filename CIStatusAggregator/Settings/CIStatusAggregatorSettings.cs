namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Settings for the application.
    /// </summary>
    public record CIStatusAggregatorSettings
    {
        public required List<EndpointSettings> Endpoints { get; init; }
    }

}
