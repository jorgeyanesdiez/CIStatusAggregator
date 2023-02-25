using CIStatusAggregator.Commons.Abstractions;
using Newtonsoft.Json;

namespace CIStatusAggregator.Commons.Services
{

    /// <summary>
    /// File serializer using Newtonsoft.Json
    /// </summary>
    public class NewtonsoftJsonFileSerializer<TObject>
        : IFileSerializer<TObject>
    {

        /// <summary>
        /// The path to the backing file for this instance.
        /// </summary>
        private string FilePath { get; }


        /// <summary>
        /// The settings used during serialization and deserialization.
        /// </summary>
        private JsonSerializerSettings Settings { get; }


        /// <summary>
        /// Main constructor.
        /// </summary>
        /// <param name="filePath">The value for the <see cref="FilePath"/> property.</param>
        /// <param name="settings">The value for the <see cref="Settings"/> property.</param>
        /// <exception cref="ArgumentNullException">If a required dependency is not provided.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If a required dependency is not valid.</exception>
        public NewtonsoftJsonFileSerializer(string filePath, JsonSerializerSettings settings)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            if (string.IsNullOrWhiteSpace(filePath)) { throw new ArgumentOutOfRangeException(nameof(filePath)); }
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }


        /// <inheritdoc/>
        public TObject? Deserialize()
        {
            TObject? result;
            try
            {
                using var reader = new StreamReader(FilePath);
                var contents = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<TObject>(contents, Settings);
            }
            catch (Exception e) when (e is FileNotFoundException || e is JsonReaderException)
            {
                result = default;
            }
            return result;
        }


        /// <inheritdoc/>
        public void Serialize(TObject input)
        {
            var contents = JsonConvert.SerializeObject(input, Settings);
            using var writer = new StreamWriter(FilePath);
            writer.Write(contents);
        }

    }

}
