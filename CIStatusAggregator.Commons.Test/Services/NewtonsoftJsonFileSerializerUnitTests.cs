using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace CIStatusAggregator.Commons.Services
{

    [Trait("TestType", "Unit")]
    public class NewtonsoftJsonFileSerializerUnitTests
    {

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        public void Constructor_InvalidFilePath_Throws(string input)
        {
            Action action = () => new NewtonsoftJsonFileSerializer<object>(input, new JsonSerializerSettings());
            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

    }

}
