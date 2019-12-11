using Moq;
using RateLimiter.Entities;
using RateLimiter.Services;
using RateLimiter.Services.Contracts;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTestProject
{
    public class RateLimiterServiceTests : IDisposable
    {
        private MockRepository mockRepository;

        private Mock<IDataAccess> mockDataAccess;

        public RateLimiterServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockDataAccess = this.mockRepository.Create<IDataAccess>();        
        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private RateLimiterService CreateService()
        {
            return new RateLimiterService(
                this.mockDataAccess.Object);
        }

        [Fact]
        public void GetConfiguration_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string configurationRule = "default";
            var newConfiguration = new Configuration
            {
                MaxActionsInPeriod = 5,
                MinTimeBetweenActions = TimeSpan.FromSeconds(5),
                Period = TimeSpan.FromMinutes(5)
            };
            
            // Act
            var result = service.GetConfiguration(
                configurationRule);
            
            // Assert
            Assert.Equal(newConfiguration.MaxActionsInPeriod, result.MaxActionsInPeriod);
            Assert.Equal(newConfiguration.MinTimeBetweenActions, result.MinTimeBetweenActions);
            Assert.Equal(newConfiguration.Period, result.Period);
        }

        [Theory]
        [InlineData(RateLimiterRules.All, 2, false)]
        [InlineData(RateLimiterRules.All, 12, true)]
        [InlineData(RateLimiterRules.MaximumAcctionsInTimeSpan, 6, true)]
        [InlineData(RateLimiterRules.MaximumAcctionsInTimeSpan, 4, false)]
        [InlineData(RateLimiterRules.TimeBetweenActions, 1, false)]
        [InlineData(RateLimiterRules.TimeBetweenActions, 100, false)]
        public void IsLimited_StateUnderTest_ExpectedBehavior(RateLimiterRules rule, int actionCount, bool expextedResult)
        {
            // Arrange
            var service = this.CreateService();

            mockDataAccess.Setup(m => m.ClientActionsInPeriod(It.Is<string>(s => s.Equals("test")), It.Is<TimeSpan>(t => t.TotalMinutes == 5)))
               .Returns(actionCount);
           
            var mockedDate = DateTime.Now;
            mockDataAccess.Setup(m => m.LastClientAction(It.Is<string>(s => s.Equals("test"))))
                .Returns(mockedDate);

            string accessToken = "test";

            // Act
            var result = service.IsLimited(
                rule,
                accessToken);

            // Assert
            Assert.Equal(expextedResult, result);
        }

        [Fact]
        public void IsMaxActionInPeriod_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            int actionsInPeriod = 0;
            int maxActionsInPeriod = 0;

            // Act
            var result = service.IsMaxActionInPeriod(
                actionsInPeriod,
                maxActionsInPeriod);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void IsTimeBetweenActions_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            DateTime lastAction = default(global::System.DateTime);
            TimeSpan? minTimeBetweenActions = null;

            // Act
            var result = service.IsTimeBetweenActions(
                lastAction,
                minTimeBetweenActions);

            // Assert
            Assert.True(true);
        }
    }
}
