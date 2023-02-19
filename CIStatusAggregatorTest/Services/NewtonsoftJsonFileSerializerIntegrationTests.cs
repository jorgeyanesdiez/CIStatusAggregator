using System.IO;
using CIStatusAggregator.Factories;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace CIStatusAggregator.Services
{

    [Trait("TestType", "Integration")]
    public class NewtonsoftJsonFileSerializerIntegrationTests
    {

        private JsonSerializerSettings Settings { get; }


        public NewtonsoftJsonFileSerializerIntegrationTests()
        {
            Settings = NewtonsoftJsonSerializerSettingsFactory.Build();
        }


        [Fact]
        public void Serialize_SerializationExample_CreatesExpectedFile()
        {
            var inputPath = "SerializationExample.input.json";
            var outputPath = "SerializationExample.output.json";
            File.Delete(outputPath);

            File.Exists(outputPath).Should().BeFalse();
            var sut = new NewtonsoftJsonFileSerializer(outputPath, Settings);
            sut.Serialize(new SerializationExample());
            File.Exists(outputPath).Should().BeTrue();

            var inputText = File.ReadAllText(inputPath);
            var outputText = File.ReadAllText(outputPath);
            outputText.Should().BeEquivalentTo(inputText);
        }


        private class SerializationExample
        {
            public string First { get; set; } = "first";
            public bool Second { get; set; } = true;
            public ExampleEnumeration Third { get; set; } = ExampleEnumeration.Third;
        }


        private enum ExampleEnumeration
        {
            First,
            Second,
            Third
        }

    }

}
