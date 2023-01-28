using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            EndpointRemoteSettings = endpointRemoteSettings ?? throw new ArgumentNullException(nameof(endpointRemoteSettings));
            _ = new Uri(EndpointRemoteSettings.BaseUrl); // Eagerly raise an exception if the URI is invalid.
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
            var status = colors.Any(c => c.EndsWith("anime")) ? CIActivityStatus.Building : CIActivityStatus.Idle;
            return status;
        }


        /// <summary>
        /// Determines the CI build status from the given colors.
        /// </summary>
        /// <param name="colors">The colors to derive the status from.</param>
        /// <returns>The requested build status.</returns>
        public CIBuildStatus GetBuildStatus(IEnumerable<string> colors)
        {
            var status = colors.Any(c => !c.StartsWith("blue")) ? CIBuildStatus.Broken : CIBuildStatus.Stable;
            return status;
        }


        /// <summary>
        /// Queries the endpoint to get the colors for the builds.
        /// The colors in Jenkins can be used to derive the activity and build status values.
        /// </summary>
        /// <returns>The collection of colors.</returns>
        public async Task<IEnumerable<string>> GetJobColorsAsync()
        {
            var r = RegexOptions.None;
            var t = TimeSpan.FromSeconds(1);

            var baseUri = new Uri(EndpointRemoteSettings.BaseUrl);
            var endpoint = new Uri(baseUri, "/api/json").ToString();
            var response = await endpoint.WithTimeout(5).GetJsonAsync<JenkinsOverview>();
            var jobs = response.Jobs;

            if (!string.IsNullOrWhiteSpace(EndpointRemoteSettings.JobNameFilterRegex))
            {
                Func<string, string, bool> jobFilter = EndpointRemoteSettings.JobNameFilterMode == RegexFilterMode.Blacklist
                    ? (i, p) => !Regex.IsMatch(i, p, r, t)
                    : (i, p) => Regex.IsMatch(i, p, r, t);

                jobs = jobs.Where(j => jobFilter(j.Name, EndpointRemoteSettings.JobNameFilterRegex));
            }

            var colors = jobs.Select(j => j.Color).Where(c => !Regex.IsMatch(c, "^(grey|disabled|aborted|notbuilt)", r, t));
            return colors;
        }

    }

}
