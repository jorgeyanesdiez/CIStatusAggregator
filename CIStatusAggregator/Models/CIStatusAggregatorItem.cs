using System.Threading.Tasks;
using CIStatusAggregator.Abstractions;

namespace CIStatusAggregator.Models
{

    /// <summary>
    /// Represents the components required to process each defined endpoint.
    /// </summary>
    public class CIStatusAggregatorItem
    {

        /// <summary>
        /// Arbitrary description of this item.
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Service to process the remote part of the endpoint.
        /// </summary>
        public IStatusProvider<Task<CIStatus>> RemoteProcessor { get; set; }


        /// <summary>
        /// Service to process the local part of the endpoint.
        /// </summary>
        public ISerializer LocalProcessor { get; set; }

    }

}
