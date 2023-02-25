namespace CIStatusAggregator.Dtos
{

    /// <summary>
    /// DTO for a Jenkins job.
    /// </summary>
    public record JenkinsJob
    {
        public required string Name { get; init; }
        public required string Color { get; init; }
    }

}
