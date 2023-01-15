using System.IO;
using System.Threading.Tasks;
using CIStatusAggregator.Models;
using FluentAssertions;
using Flurl;
using Flurl.Http.Testing;
using Xunit;

namespace CIStatusAggregator.Services
{

    [Trait("TestType", "Integration")]
    public class JenkinsStatusProviderIntegrationTests
    {

        public static readonly string url = "http://localhost";


        [Theory]
        [InlineData("BlueYellowGrey")]
        public async Task GetStatus_IdleJobs_IsActivityStatusExpected(string partialName)
        {
            var response = File.ReadAllText($"Jenkins.{partialName}.json");
            using var httpTest = new HttpTest();
            httpTest.RespondWith(response, 200);
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = await sut.GetStatus();
            result.ActivityStatus.Should().Be(CIActivityStatus.Idle);
            httpTest.ShouldHaveCalled(Url.Combine(url, "*"));
        }


        [Theory]
        [InlineData("BlueAnimeYellowGrey")]
        [InlineData("BlueBlueAnime")]
        public async Task GetStatus_BuildingJobs_IsActivityStatusExpected(string partialName)
        {
            var response = File.ReadAllText($"Jenkins.{partialName}.json");
            using var httpTest = new HttpTest();
            httpTest.RespondWith(response, 200);
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = await sut.GetStatus();
            result.ActivityStatus.Should().Be(CIActivityStatus.Building);
            httpTest.ShouldHaveCalled(Url.Combine(url, "*"));
        }


        [Theory]
        [InlineData("BlueBlueAnime")]
        public async Task GetStatus_StableJobs_IsBuildStatusExpected(string partialName)
        {
            var response = File.ReadAllText($"Jenkins.{partialName}.json");
            using var httpTest = new HttpTest();
            httpTest.RespondWith(response, 200);
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = await sut.GetStatus();
            result.BuildStatus.Should().Be(CIBuildStatus.Stable);
            httpTest.ShouldHaveCalled(Url.Combine(url, "*"));
        }


        [Theory]
        [InlineData("BlueRed")]
        [InlineData("BlueYellowGrey")]
        public async Task GetStatus_BrokenJobs_IsBuildStatusExpected(string partialName)
        {
            var response = File.ReadAllText($"Jenkins.{partialName}.json");
            using var httpTest = new HttpTest();
            httpTest.RespondWith(response, 200);
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = await sut.GetStatus();
            result.BuildStatus.Should().Be(CIBuildStatus.Broken);
            httpTest.ShouldHaveCalled(Url.Combine(url, "*"));
        }


        [Fact]
        public async Task GetJobColorsAsync_BlueYellowGrey_IsExpected()
        {
            var response = File.ReadAllText("Jenkins.BlueYellowGrey.json");
            using var httpTest = new HttpTest();
            httpTest.RespondWith(response, 200);
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings() { BaseUrl = url });
            var result = await sut.GetJobColorsAsync();
            result.Should().Contain("blue", "yellow");
            result.Should().NotContain("grey");
            httpTest.ShouldHaveCalled(Url.Combine(url, "*"));
        }


        [Fact]
        public async Task GetJobColorsAsync_FilteredBlacklist_IsExpected()
        {
            var response = File.ReadAllText("Jenkins.BlueYellowGrey.json");
            using var httpTest = new HttpTest();
            httpTest.RespondWith(response, 200);
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings()
            {
                BaseUrl = url,
                JobNameFilterMode = RegexFilterMode.Blacklist,
                JobNameFilterRegex = "JOB1"
            });
            var result = await sut.GetJobColorsAsync();
            result.Should().Contain("yellow");
            result.Should().NotContain("grey");
            result.Should().HaveCount(1);
            httpTest.ShouldHaveCalled(Url.Combine(url, "*"));
        }


        [Fact]
        public async Task GetJobColorsAsync_FilteredWhitelist_IsExpected()
        {
            var response = File.ReadAllText("Jenkins.BlueYellowGrey.json");
            using var httpTest = new HttpTest();
            httpTest.RespondWith(response, 200);
            var sut = new JenkinsStatusProvider(new Settings.EndpointRemoteSettings()
            {
                BaseUrl = url,
                JobNameFilterMode = RegexFilterMode.Whitelist,
                JobNameFilterRegex = "JOB1"
            });
            var result = await sut.GetJobColorsAsync();
            result.Should().Contain("blue");
            result.Should().HaveCount(1);
            httpTest.ShouldHaveCalled(Url.Combine(url, "*"));
        }

    }

}
