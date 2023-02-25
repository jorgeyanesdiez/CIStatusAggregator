namespace CIStatusAggregator.Dtos
{

    /// <summary>
    /// DTO for the global overview of a Jenkins instance.
    /// </summary>
    public record JenkinsOverview
    {
        public required IEnumerable<JenkinsJob> Jobs { get; init; }
    }

}
