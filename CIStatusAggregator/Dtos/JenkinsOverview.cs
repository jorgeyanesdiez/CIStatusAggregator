using System.Collections.Generic;
using System.Linq;

namespace CIStatusAggregator.Dtos
{

    /// <summary>
    /// Incoming DTO for the global overview of a Jenkins instance.
    /// </summary>
    public class JenkinsOverview
    {
        public IEnumerable<JenkinsJob> Jobs { get; set; } = Enumerable.Empty<JenkinsJob>();
    }

}
