namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Settings for an endpoint.
    /// </summary>
    public class EndpointSettings
    {
        public EndpointMetaSettings Meta { get; set; }
        public EndpointRemoteSettings Remote { get; set; }
        public EndpointLocalSettings Local { get; set; }
    }

}
