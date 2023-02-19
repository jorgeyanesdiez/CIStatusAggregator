using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CIStatusAggregator.Factories
{

    /// <summary>
    /// Factory that creates Newtonsoft.Json serializer settings.
    /// </summary>
    public static class NewtonsoftJsonSerializerSettingsFactory
    {

        /// <summary>
        /// Builds an object that represents the desired settings for Newtonsoft.Json serializers.
        /// </summary>
        /// <returns>The requested object.</returns>
        public static JsonSerializerSettings Build()
        {
            var jsonSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            jsonSettings.Converters.Add(new StringEnumConverter());
            return jsonSettings;
        }

    }

}
