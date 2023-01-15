using System;
using CIStatusAggregator.Models;
using FluentAssertions;
using Xunit;

namespace CIStatusAggregator.Services
{

    [Trait("TestType", "Unit")]
    public class JenkinsStatusProviderUnitTests
    {

        public static readonly string url = "http://localhost";


        [Fact]
        public void Constructor_Null_Throws()
        {
            Action action = () => new JenkinsStatusProvider(null);
            action.Should().ThrowExactly<ArgumentNullException>();
        }


        [Theory]
        [InlineData("invalidUri")]
        public void Constructor_Invalid_Throws(string invalidUri)
        {
            Action action = () => new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = invalidUri });
            action.Should().ThrowExactly<UriFormatException>();
        }


        [Fact]
        public void GetActivityStatus_Empty_IsExpected()
        {
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = sut.GetActivityStatus(Array.Empty<string>());
            result.Should().Be(CIActivityStatus.Idle);
        }


        [Theory]
        [InlineData("red")]
        [InlineData("yellow")]
        [InlineData("blue")]
        [InlineData("grey")]
        [InlineData("disabled")]
        [InlineData("aborted")]
        [InlineData("notbuilt")]
        [InlineData("UNEXPECTED")]
        public void GetActivityStatus_RegularColors_IsExpected(string regularColor)
        {
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = sut.GetActivityStatus(new[] { regularColor });
            result.Should().Be(CIActivityStatus.Idle);
        }


        [Theory]
        [InlineData("red_anime")]
        [InlineData("yellow_anime")]
        [InlineData("blue_anime")]
        [InlineData("grey_anime")]
        [InlineData("disabled_anime")]
        [InlineData("aborted_anime")]
        [InlineData("notbuilt_anime")]
        [InlineData("UNEXPECTED_anime")]
        public void GetActivityStatus_AnimatedColors_IsExpected(string animatedColor)
        {
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = sut.GetActivityStatus(new[] { animatedColor });
            result.Should().Be(CIActivityStatus.Building);
        }


        [Theory]
        [InlineData("red", "red_anime")]
        [InlineData("UNEXPECTED", "UNEXPECTED_anime")]
        public void GetActivityStatus_MixedColors_IsExpected(string regularColor, string animatedColor)
        {
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = sut.GetActivityStatus(new[] { regularColor, animatedColor });
            result.Should().Be(CIActivityStatus.Building);
        }


        [Fact]
        public void GetBuildStatus_Empty_IsExpected()
        {
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = sut.GetBuildStatus(Array.Empty<string>());
            result.Should().Be(CIBuildStatus.Stable);
        }


        [Theory]
        [InlineData("blue")]
        [InlineData("blue_anime")]
        public void GetBuildStatus_StableColors_IsExpected(string stableColor)
        {
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = sut.GetBuildStatus(new[] { stableColor });
            result.Should().Be(CIBuildStatus.Stable);
        }


        [Theory]
        [InlineData("red")]
        [InlineData("yellow")]
        [InlineData("grey")]
        [InlineData("disabled")]
        [InlineData("aborted")]
        [InlineData("notbuilt")]
        [InlineData("UNEXPECTED")]
        [InlineData("red_anime")]
        [InlineData("yellow_anime")]
        [InlineData("grey_anime")]
        [InlineData("disabled_anime")]
        [InlineData("aborted_anime")]
        [InlineData("notbuilt_anime")]
        [InlineData("UNEXPECTED_anime")]
        public void GetBuildStatus_BrokenColors_IsExpected(string brokenColor)
        {
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = sut.GetBuildStatus(new[] { brokenColor });
            result.Should().Be(CIBuildStatus.Broken);
        }


        [Theory]
        [InlineData("blue", "red")]
        [InlineData("blue_anime", "red_anime")]
        [InlineData("UNEXPECTED", "UNEXPECTED_anime")]
        public void GetBuildStatus_MixedColors_IsExpected(string regularColor, string animatedColor)
        {
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = sut.GetBuildStatus(new[] { regularColor, animatedColor });
            result.Should().Be(CIBuildStatus.Broken);
        }

    }

}
