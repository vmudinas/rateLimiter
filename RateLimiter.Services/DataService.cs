using RateLimiter.Services.Contracts;
using RateLimiter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RateLimiter.Services
{
    public class DataService : IDataAccess
    {
        public DataService(ClientsData data)
        {
            this.data = data;
        }

        private ClientsData data;

        public IEnumerable<ClientData> GetClientData(string accessToken)
        {
            return this.data.ClientsDataList.Where(x => x.AccessToken == accessToken);
        }

        public DateTime LastClientAction(string accessToken)
        {
           return this.GetClientData(accessToken).Max(d=>d.AccessTime);
        }

        public int ClientActionsInPeriod(string accessToken, TimeSpan timespan)
        {
            return this.GetClientData(accessToken).Count(d => d.AccessTime < DateTime.UtcNow && d.AccessTime > DateTime.UtcNow.Add(-timespan) );
        }
    }

}
