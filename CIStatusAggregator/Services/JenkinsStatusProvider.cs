using System.Text.RegularExpressions;
using CIStatusAggregator.Abstractions;
using CIStatusAggregator.Dtos;
using CIStatusAggregator.Models;
using CIStatusAggregator.Settings;
using Flurl.Http;

namespace CIStatusAggregator.Services
{

    /// <summary>
    /// Queries a Jenkins endpoint to determine various status aggregates.
    /// </summary>
    /// <seealso href="https://github.com/jenkinsci/jenkins/blob/master/core/src/main/java/hudson/model/BallColor.java"/>
    public class JenkinsStatusProvider
        : IStatusProvider<Task<CIStatus>>
    {

        /// <summary>
        /// The settings for the endpoint.
        /// </summary>
        private EndpointRemoteSettings EndpointRemoteSettings { get; }


        /// <summary>
        /// Main constructor.
        /// </summary>
        /// <param name="endpointRemoteSettings">The value for the <see cref="EndpointRemoteSettings"/> property.</param>
        public JenkinsStatusProvider(EndpointRemoteSettings endpointRemoteSettings)
        {
            EndpointRemoteSettings = endpointRemoteSettings;
        }


        /// <inheritdoc/>
        public async Task<CIStatus> GetStatus()
        {
            var colors = await GetJobColorsAsync();
            var status = new CIStatus()
            {
                ActivityStatus = GetActivityStatus(colors),
                BuildStatus = GetBuildStatus(colors)
            };
            return status;
        }


        /// <summary>
        /// Determines the CI activity status from the given colors.
        /// </summary>
        /// <param name="colors">The colors to derive the status from.</param>
        /// <returns>The requested activity status.</returns>
        public CIActivityStatus GetActivityStatus(IEnumerable<string> colors)
        {
            var status = colors.Any(color => color.EndsWith("anime")) ? CIActivityStatus.Building : CIActivityStatus.Idle;
            return status;
        }


        /// <summary>
        /// Determines the CI build status from the given colors.
        /// </summary>
        /// <param name="colors">The colors to derive the status from.</param>
        /// <returns>The requested build status.</returns>
        public CIBuildStatus GetBuildStatus(IEnumerable<string> colors)
        {
            var status = colors.Any(color => !color.StartsWith("blue")) ? CIBuildStatus.Broken : CIBuildStatus.Stable;
            return status;
        }


        /// <summary>
        /// Queries the endpoint to get the colors for the builds.
        /// The colors in Jenkins can be used to derive the activity and build status values.
        /// </summary>
        /// <returns>The collection of colors.</returns>
        public async Task<IEnumerable<string>> GetJobColorsAsync()
        {
            var opts = RegexOptions.None;
            var timeout = TimeSpan.FromSeconds(1);

            var baseUri = new Uri(EndpointRemoteSettings.BaseUrl);
            var endpoint = new Uri(baseUri, "/api/json").ToString();
            var response = await endpoint.WithTimeout(timeout).GetJsonAsync<JenkinsOverview>();
            var jobs = response.Jobs;

            if (EndpointRemoteSettings.JobNameFilter != null)
            {
                Func<string, string, bool> jobFilter = EndpointRemoteSettings.JobNameFilter.Mode == RegexFilterMode.Blacklist
                    ? (input, pattern) => !Regex.IsMatch(input, pattern, opts, timeout)
                    : (input, pattern) => Regex.IsMatch(input, pattern, opts, timeout);

                jobs = jobs.Where(job => jobFilter(job.Name, EndpointRemoteSettings.JobNameFilter.Regex));
            }

            var colors = jobs.Select(job => job.Color).Where(color => !Regex.IsMatch(color, "^(grey|disabled|aborted|notbuilt)", opts, timeout));
            return colors;
        }

    }

}
