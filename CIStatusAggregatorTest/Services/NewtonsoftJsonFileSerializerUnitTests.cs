using System;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace CIStatusAggregator.Services
{

    [Trait("TestType", "Unit")]
    public class NewtonsoftJsonFileSerializerUnitTests
    {

        [Fact]
        public void Constructor_NullFilePath_Throws()
        {
            Action action = () => new NewtonsoftJsonFileSerializer(null, new JsonSerializerSettings());
            action.Should().ThrowExactly<ArgumentNullException>();
        }


        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        public void Constructor_InvalidFilePath_Throws(string input)
        {
            Action action = () => new NewtonsoftJsonFileSerializer(input, new JsonSerializerSettings());
            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }


        [Fact]
        public void Constructor_NullSettings_Throws()
        {
            Action action = () => new NewtonsoftJsonFileSerializer("test", null);
            action.Should().ThrowExactly<ArgumentNullException>();
        }

    }

}
