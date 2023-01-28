using System;
using System.IO;
using CIStatusAggregator.Abstractions;
using Newtonsoft.Json;

namespace CIStatusAggregator.Services
{

    /// <summary>
    /// File serializer using Json.Net
    /// </summary>
    public class JsonNetFileSerializer
        : ISerializer
    {

        /// <summary>
        /// The path for the serialization operations of this instance.
        /// </summary>
        private string FilePath { get; }


        /// <summary>
        /// Main constructor.
        /// </summary>
        /// <param name="filePath">The value for the <see cref="FilePath"/> property.</param>
        /// <exception cref="ArgumentNullException">If a required dependency is not provided.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If a required dependency is not valid.</exception>
        public JsonNetFileSerializer(string filePath)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            if (string.IsNullOrWhiteSpace(filePath)) { throw new ArgumentOutOfRangeException(nameof(filePath)); }
        }


        /// <inheritdoc/>
        public void Serialize<TObject>(TObject input) where TObject : new()
        {
            var contents = JsonConvert.SerializeObject(input);
            using var writer = new StreamWriter(FilePath);
            writer.Write(contents);
        }

    }

}
