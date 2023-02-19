namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Settings for an endpoint.
    /// </summary>
    public class EndpointSettings
    {
        public EndpointMetaSettings Meta { get; set; } = new EndpointMetaSettings();
        public EndpointRemoteSettings Remote { get; set; } = new EndpointRemoteSettings();
        public EndpointLocalSettings Local { get; set; } = new EndpointLocalSettings();
    }

}
