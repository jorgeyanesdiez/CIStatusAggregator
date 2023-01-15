namespace CIStatusAggregator.Abstractions
{

    /// <summary>
    /// Contract for serialization services.
    /// </summary>
    public interface ISerializer
    {

        /// <summary>
        /// Writes the given instance.
        /// </summary>
        /// <typeparam name="TInput">The type of the object to write.</typeparam>
        /// <param name="input">The instance to write.</param>
        void Serialize<TInput>(TInput input) where TInput : new();

    }

}
