using FluentAssertions;
using Xunit;

namespace CIStatusAggregator.Settings
{

    [Trait("TestType", "Unit")]
    public class EndpointSettingsUnitTests
    {

        [Fact]
        public void Meta_ByDefault_IsNotNull()
        {
            var sut = new EndpointSettings();
            sut.Meta.Should().NotBeNull();
        }

        [Fact]
        public void Remote_ByDefault_IsNotNull()
        {
            var sut = new EndpointSettings();
            sut.Remote.Should().NotBeNull();
        }

        [Fact]
        public void Local_ByDefault_IsNotNull()
        {
            var sut = new EndpointSettings();
            sut.Local.Should().NotBeNull();
        }

    }

}
