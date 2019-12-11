using RateLimiter.Entities;
using System;
using Xunit;

namespace UnitTestProject
{
    public class ConfigurationTests
    {
        [Fact]
        public void ConfigurationEntityTests()
        {
            // Arrange
            var maxInPeriod = 5;
            var minBetweenActions = TimeSpan.FromSeconds(1);
            var period = TimeSpan.FromSeconds(2);
            // Act
            var configuration = new Configuration();
            configuration.MaxActionsInPeriod = maxInPeriod;
            configuration.MinTimeBetweenActions = minBetweenActions;
            configuration.Period = period;

            // Assert
            Assert.Equal(maxInPeriod, configuration.MaxActionsInPeriod);
            Assert.Equal(minBetweenActions, configuration.MinTimeBetweenActions);
            Assert.Equal(period, configuration.Period);
        }
    }
}
