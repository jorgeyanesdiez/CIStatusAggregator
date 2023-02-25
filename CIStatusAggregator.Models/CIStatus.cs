namespace CIStatusAggregator.Models
{

    /// <summary>
    /// Models the status of a CI system.
    /// </summary>
    public record CIStatus
    {

        /// <summary>
        /// The activity status of the CI system.
        /// </summary>
        public required CIActivityStatus ActivityStatus { get; init; }


        /// <summary>
        /// The build status of the CI system.
        /// </summary>
        public required CIBuildStatus BuildStatus { get; init; }

    }

}
