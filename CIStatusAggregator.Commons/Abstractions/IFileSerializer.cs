namespace CIStatusAggregator.Commons.Abstractions
{

    /// <summary>
    /// Contract for serialization services that work with files.
    /// Introduces a common type parameter for all methods for coherence.
    /// </summary>
    /// <typeparam name="TObject">The type of the object to work with.</typeparam>
    public interface IFileSerializer<TObject>
    {

        /// <summary>
        /// Reads a serialized object.
        /// </summary>
        /// <returns>The deserialized object.</returns>
        TObject? Deserialize();


        /// <summary>
        /// Writes the given object.
        /// </summary>
        /// <param name="input">The object to write.</param>
        void Serialize(TObject input);

    }

}
