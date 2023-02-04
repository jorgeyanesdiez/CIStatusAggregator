using FluentAssertions;
using Xunit;

namespace CIStatusAggregator.Settings
{

    [Trait("TestType", "Unit")]
    public class CIStatusAggregatorSettingsUnitTests
    {

        [Fact]
        public void Endpoints_ByDefault_IsNotNull()
        {
            var sut = new CIStatusAggregatorSettings();
            sut.Endpoints.Should().NotBeNull();
        }

    }
}
