using System;
using System.IO;
using CIStatusAggregator.Abstractions;
using Newtonsoft.Json;

namespace CIStatusAggregator.Services
{

    /// <summary>
    /// File serializer using Newtonsoft.Json
    /// </summary>
    public class NewtonsoftJsonFileSerializer
        : ISerializer
    {

        /// <summary>
        /// The path for the serialization operations of this instance.
        /// </summary>
        private string FilePath { get; }


        /// <summary>
        /// The settings used during serialization.
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
        public void Serialize<TObject>(TObject input) where TObject : new()
        {
            var contents = JsonConvert.SerializeObject(input, Settings);
            using var writer = new StreamWriter(FilePath);
            writer.Write(contents);
        }

    }

}
