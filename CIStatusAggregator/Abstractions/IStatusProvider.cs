namespace CIStatusAggregator.Abstractions
{

    /// <summary>
    /// Contract for services that provide a status.
    /// </summary>
    /// <typeparam name="TStatus">The type of the status.</typeparam>
    public interface IStatusProvider<out TStatus>
    {

        /// <summary>
        /// Provides a status.
        /// </summary>
        /// <returns>The requested value.</returns>
        TStatus GetStatus();

    }

}
