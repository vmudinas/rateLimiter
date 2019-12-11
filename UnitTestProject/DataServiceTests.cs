using Moq;
using RateLimiter.Entities;
using RateLimiter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTestProject
{
    public class DataServiceTests
    {    


        private DataService CreateService()
        {
            var testData = new ClientsData();
            testData.ClientsDataList = new List<ClientData>
            {
                new ClientData { AccessTime = DateTime.Now, AccessToken = "test" },
                new ClientData { AccessTime = DateTime.Now.AddSeconds(1), AccessToken = "test" },
                new ClientData { AccessTime = DateTime.Now.AddSeconds(2), AccessToken = "test" },
                new ClientData { AccessTime = DateTime.Now.AddSeconds(3), AccessToken = "test" },
                new ClientData { AccessTime = DateTime.Now.AddSeconds(1), AccessToken = "test" },
                new ClientData { AccessTime = DateTime.Now.AddDays(10), AccessToken = "test" },
                new ClientData { AccessTime = DateTime.Now.AddSeconds(1), AccessToken = "test2" },
                new ClientData { AccessTime = DateTime.Now.AddSeconds(3), AccessToken = "test2" }
            };

            return new DataService(testData);
        }

        [Fact]
        public void GetClientData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string accessToken = "test";

            // Act
            var result = service.GetClientData(
                accessToken);

            // Assert
            Assert.Equal(6, result.Count());
        }

        [Fact]
        public void LastClientAction_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string accessToken = "test";
            var currentTime = DateTime.Now.AddDays(10);
            // Act
            var result = service.LastClientAction(
                accessToken);

            // Assert
            Assert.Equal(currentTime.Date, result.Date);
        }

        [Fact]
        public void ClientActionsInPeriod_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string accessToken = "test";
            TimeSpan timespan = TimeSpan.FromDays(1);
            var expectedAcctions = 5;
            // Act
            var result = service.ClientActionsInPeriod(
                accessToken,
                timespan);

            // Assert
            Assert.Equal(expectedAcctions, result);
        }
    }
}
