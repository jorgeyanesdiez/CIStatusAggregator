using FluentAssertions;
using Xunit;

namespace CIStatusAggregator.Settings
{

    [Trait("TestType", "Unit")]
    public class AppSettingsUnitTests
    {

        [Fact]
        public void CIStatusAggregator_ByDefault_IsNotNull()
        {
            var sut = new AppSettings();
            sut.CIStatusAggregator.Should().NotBeNull();
        }

    }
}
