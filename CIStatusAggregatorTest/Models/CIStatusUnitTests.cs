using FluentAssertions;
using Xunit;

namespace CIStatusAggregator.Models
{

    [Trait("TestType", "Unit")]
    public class CIStatusUnitTests
    {

        [Fact]
        public void Equals_Null_IsExpected()
        {
            var sut = new CIStatus();
            var result = sut.Equals(null);
            result.Should().BeFalse();
        }


        [Theory]
        [InlineData(CIActivityStatus.Idle, CIBuildStatus.Stable)]
        [InlineData(CIActivityStatus.Idle, CIBuildStatus.Broken)]
        [InlineData(CIActivityStatus.Building, CIBuildStatus.Stable)]
        [InlineData(CIActivityStatus.Building, CIBuildStatus.Broken)]
        public void Equals_Self_IsExpected(CIActivityStatus activityStatus, CIBuildStatus buildStatus)
        {
            var sut = new CIStatus { ActivityStatus = activityStatus, BuildStatus = buildStatus };
            var result = sut.Equals(sut);
            result.Should().BeTrue();
        }


        [Theory]
        [InlineData(CIActivityStatus.Idle, CIBuildStatus.Stable)]
        [InlineData(CIActivityStatus.Idle, CIBuildStatus.Broken)]
        [InlineData(CIActivityStatus.Building, CIBuildStatus.Stable)]
        [InlineData(CIActivityStatus.Building, CIBuildStatus.Broken)]
        public void Equals_Same_IsExpected(CIActivityStatus activityStatus, CIBuildStatus buildStatus)
        {
            var sut = new CIStatus { ActivityStatus = activityStatus, BuildStatus = buildStatus };
            var target = new CIStatus { ActivityStatus = activityStatus, BuildStatus = buildStatus };
            var result = sut.Equals(target);
            result.Should().BeTrue();
        }


        [Theory]
        [InlineData(CIBuildStatus.Stable)]
        [InlineData(CIBuildStatus.Broken)]
        public void Equals_DifferentActivityStatus_IsExpected(CIBuildStatus buildStatus)
        {
            var sut = new CIStatus { ActivityStatus = CIActivityStatus.Idle, BuildStatus = buildStatus };
            var target = new CIStatus { ActivityStatus = CIActivityStatus.Building, BuildStatus = buildStatus };
            var result = sut.Equals(target);
            result.Should().BeFalse();
        }


        [Theory]
        [InlineData(CIActivityStatus.Idle)]
        [InlineData(CIActivityStatus.Building)]
        public void Equals_DifferentBuildStatus_IsExpected(CIActivityStatus activityStatus)
        {
            var sut = new CIStatus { ActivityStatus = activityStatus, BuildStatus = CIBuildStatus.Stable };
            var target = new CIStatus { ActivityStatus = activityStatus, BuildStatus = CIBuildStatus.Broken };
            var result = sut.Equals(target);
            result.Should().BeFalse();
        }

    }

}
