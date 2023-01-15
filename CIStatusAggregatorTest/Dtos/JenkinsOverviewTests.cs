using FluentAssertions;
using Xunit;

namespace CIStatusAggregator.Dtos
{

    [Trait("TestType", "Unit")]
    public class JenkinsOverviewTests
    {

        [Fact]
        public void Jobs_ByDefault_IsNotNull()
        {
            var sut = new JenkinsOverview();
            sut.Jobs.Should().NotBeNull();
        }

    }
}
