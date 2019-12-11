using RateLimiter.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTestProject
{
    public class ClientsDataTests
    {
        [Fact]
        public void ClientsDataTest()
        {
            // Arrange
            var clientsData = new ClientsData();
            var clientData = new ClientData
            {
                AccessTime = DateTime.Now,
                AccessToken = "token"
            };
            clientsData.ClientsDataList = new List<ClientData>
            {
                // Act
                clientData
            };

            // Assert
            Assert.Single(clientsData.ClientsDataList);
        }
    }
}
