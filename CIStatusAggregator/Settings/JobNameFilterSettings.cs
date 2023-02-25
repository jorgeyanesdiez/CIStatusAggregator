using CIStatusAggregator.Models;

namespace CIStatusAggregator.Settings
{

    /// <summary>
    /// Settings that define a job name filter.
    /// </summary>
    public record JobNameFilterSettings
    {
        public required RegexFilterMode Mode { get; init; }
        public required string Regex { get; init; }
    }

}
