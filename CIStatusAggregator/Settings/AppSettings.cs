namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Sections of the appsettings file used by this application.
    /// </summary>
    public record AppSettings
    {
        public required CIStatusAggregatorSettings CIStatusAggregator { get; init; }
    }

}
