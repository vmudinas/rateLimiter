namespace RateLimiter.Services.Contracts
{
    using RateLimiter.Entities;
    using System;
    using System.Collections.Generic;

    public interface IDataAccess
    {
        IEnumerable<ClientData> GetClientData(string accessToken);
        DateTime LastClientAction(string accessToken);
        int ClientActionsInPeriod(string accessToken, TimeSpan timespan);

    }
}