using CIStatusAggregator.Abstractions;
using CIStatusAggregator.Commons.Abstractions;

namespace CIStatusAggregator.Models
{

    /// <summary>
    /// Represents the components required to process each defined endpoint.
    /// </summary>
    public record CIStatusAggregatorItem
    {

        /// <summary>
        /// Arbitrary description of this item.
        /// </summary>
        public required string Description { get; init; }


        /// <summary>
        /// Service to process the remote part of the endpoint.
        /// </summary>
        public required IStatusProvider<Task<CIStatus>> RemoteProcessor { get; init; }


        /// <summary>
        /// Service to process the local part of the endpoint.
        /// </summary>
        public required IFileSerializer<CIStatus> LocalProcessor { get; init; }

    }

}
