using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CIStatusAggregator.Abstractions;
using CIStatusAggregator.Models;
using FluentAssertions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CIStatusAggregator.Services
{

    [Trait("TestType", "Unit")]
    public class CIStatusAggregatorServiceUnitTests
    {

        [Fact]
        public void Constructor_NullAppLifetime_Throws()
        {
            Action action = () => new CIStatusAggregatorService(
                null,
                Mock.Of<ILogger<CIStatusAggregatorService>>(),
                new[] { new CIStatusAggregatorItem() }
            );
            action.Should().ThrowExactly<ArgumentNullException>();
        }


        [Fact]
        public void Constructor_NullLogger_Throws()
        {
            Action action = () => new CIStatusAggregatorService(
                Mock.Of<IHostApplicationLifetime>(),
                null,
                new[] { new CIStatusAggregatorItem() }
            );
            action.Should().ThrowExactly<ArgumentNullException>();
        }


        [Fact]
        public void Constructor_NullItems_Throws()
        {
            Action action = () => new CIStatusAggregatorService(
                Mock.Of<IHostApplicationLifetime>(),
                Mock.Of<ILogger<CIStatusAggregatorService>>(),
                null
            );
            action.Should().ThrowExactly<ArgumentNullException>();
        }


        [Fact]
        public void Constructor_EmptyItems_Throws()
        {
            Action action = () => new CIStatusAggregatorService(
                Mock.Of<IHostApplicationLifetime>(),
                Mock.Of<ILogger<CIStatusAggregatorService>>(),
                Enumerable.Empty<CIStatusAggregatorItem>()
            );
            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }


        [Fact]
        public async Task StartAsync_CallsExpected()
        {
            var appLifetimeMock = new Mock<IHostApplicationLifetime>();
            var loggerMock = Mock.Of<ILogger<CIStatusAggregatorService>>();
            var mockRemoteProcessorMock = new Mock<IStatusProvider<Task<CIStatus>>>();
            var mockLocalProcessorMock = new Mock<ISerializer>();
            var items = new[] { new CIStatusAggregatorItem()
            {
                Description = "Mock item",
                RemoteProcessor = mockRemoteProcessorMock.Object,
                LocalProcessor = mockLocalProcessorMock.Object
            }};

            appLifetimeMock.Setup(m => m.StopApplication()).Verifiable();
            mockRemoteProcessorMock.Setup(m => m.GetStatus()).ReturnsAsync(new CIStatus()).Verifiable();
            mockLocalProcessorMock.Setup(m => m.Serialize(It.IsAny<CIStatus>())).Verifiable();

            var sut = new CIStatusAggregatorService(
                appLifetimeMock.Object,
                loggerMock,
                items
            );

            await sut.StartAsync(new CancellationToken());

            appLifetimeMock.Verify(m => m.StopApplication(), Times.Once);
            mockRemoteProcessorMock.Verify(m => m.GetStatus(), Times.Once);
            mockLocalProcessorMock.Verify(m => m.Serialize(It.IsAny<CIStatus>()), Times.Once);
        }


        [Fact]
        public async Task StartAsync_OnException_Throws()
        {
            var appLifetimeMock = Mock.Of<IHostApplicationLifetime>();
            var loggerMock = Mock.Of<ILogger<CIStatusAggregatorService>>();
            var mockRemoteProcessorMock = new Mock<IStatusProvider<Task<CIStatus>>>();
            var mockLocalProcessorMock = Mock.Of<ISerializer>();
            var items = new[] { new CIStatusAggregatorItem()
            {
                Description = "Mock item",
                RemoteProcessor = mockRemoteProcessorMock.Object,
                LocalProcessor = mockLocalProcessorMock
            }};

            mockRemoteProcessorMock.Setup(m => m.GetStatus()).Throws<Exception>();

            var sut = new CIStatusAggregatorService(
                appLifetimeMock,
                loggerMock,
                items
            );

            Func<Task> action = async () => await sut.StartAsync(new CancellationToken());
            await action.Should().ThrowAsync<Exception>();
        }

    }

}
