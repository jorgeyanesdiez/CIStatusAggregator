namespace CIStatusAggregator.Models
{

    /// <summary>
    /// Models the status of a CI system.
    /// </summary>
    public sealed class CIStatus
    {

        /// <summary>
        /// The activity status of the system.
        /// </summary>
        public CIActivityStatus ActivityStatus { get; set; }


        /// <summary>
        /// The build status of the system.
        /// </summary>
        public CIBuildStatus BuildStatus { get; set; }

    }

}
