namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Sections of the appsettings file used by this application.
    /// </summary>
    public class AppSettings
    {
        public CIStatusAggregatorSettings CIStatusAggregator { get; set; } = new CIStatusAggregatorSettings();
    }

}
