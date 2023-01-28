namespace CIStatusAggregator.Abstractions
{

    /// <summary>
    /// Contract for serialization services.
    /// </summary>
    public interface ISerializer
    {

        /// <summary>
        /// Writes the given object.
        /// </summary>
        /// <typeparam name="TObject">The type of the object to write.</typeparam>
        /// <param name="input">The object to write.</param>
        void Serialize<TObject>(TObject input) where TObject : new();

    }

}
