namespace CIStatusAggregator.Dtos
{

    /// <summary>
    /// Incoming DTO for a Jenkins job.
    /// </summary>
    public class JenkinsJob
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }

}
