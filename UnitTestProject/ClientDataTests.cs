using RateLimiter.Entities;
using System;
using Xunit;

namespace UnitTestProject
{
    public class ClientDataTests
    {
        [Fact]
        public void ClientDataTest()
        {
            // Arrange
            var accessTime = DateTime.Now;
            var accessToken = "AccessToken";
            
            // Act
            var clientData = new ClientData
            {
                AccessTime = accessTime,
                AccessToken = accessToken
            };

            // Assert
            Assert.Equal(clientData.AccessToken, accessToken);
            Assert.Equal(clientData.AccessTime, accessTime);
        }
    }
}
