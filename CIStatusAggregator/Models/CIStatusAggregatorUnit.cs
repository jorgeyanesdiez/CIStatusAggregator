using System.Threading.Tasks;
using CIStatusAggregator.Abstractions;

namespace CIStatusAggregator.Models
{

    /// <summary>
    /// Represents the components required to process each defined endpoint.
    /// </summary>
    public class CIStatusAggregatorUnit
    {

        /// <summary>
        /// Arbitrary description of this unit.
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
