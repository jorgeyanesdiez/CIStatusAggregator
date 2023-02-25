using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CIStatusAggregator.Commons.Factories
{

    /// <summary>
    /// Factory that creates Newtonsoft.Json serializer settings.
    /// </summary>
    public static class NewtonsoftJsonSerializerSettingsFactory
    {

        /// <summary>
        /// Builds the desired settings object for projects using the classes in this namespace.
        /// </summary>
        /// <returns>The requested settings.</returns>
        public static JsonSerializerSettings Build()
        {
            var jsonSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            jsonSettings.Converters.Add(new StringEnumConverter(typeof(CamelCaseNamingStrategy)));
            jsonSettings.Formatting = Formatting.Indented;
            return jsonSettings;
        }

    }

}
