using FluentAssertions;
using Xunit;

namespace CIStatusAggregator.Settings
{

    [Trait("TestType", "Unit")]
    public class AppSettingsTests
    {

        [Fact]
        public void Endpoints_ByDefault_IsNotNull()
        {
            var sut = new AppSettings();
            sut.Endpoints.Should().NotBeNull();
        }

    }
}
