using CIStatusAggregator.Commons.Factories;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace CIStatusAggregator.Commons.Services
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
        public void Deserialize_NonExisting_IsExpected()
        {
            var filePath = "NonExistingFile";
            var sut = new NewtonsoftJsonFileSerializer<SerializationExample>(filePath, Settings);
            var result = sut.Deserialize();
            result.Should().BeNull();
        }


        [Fact]
        public void Deserialize_SerializationExample_IsExpected()
        {
            var filePath = "SerializationExample.input.json";
            var sut = new NewtonsoftJsonFileSerializer<SerializationExample>(filePath, Settings);
            var expected = new SerializationExample();
            var result = sut.Deserialize();
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public void Serialize_SerializationExample_CreatesExpected()
        {
            var inputPath = "SerializationExample.input.json";
            var outputPath = "SerializationExample.output.json";
            File.Delete(outputPath);

            File.Exists(outputPath).Should().BeFalse();
            var sut = new NewtonsoftJsonFileSerializer<SerializationExample>(outputPath, Settings);
            sut.Serialize(new SerializationExample());
            File.Exists(outputPath).Should().BeTrue();

            var inputText = File.ReadAllText(inputPath);
            var outputText = File.ReadAllText(outputPath);
            outputText.Should().Be(inputText);
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
