using RateLimiter.Services.Contracts;
using RateLimiter.Entities;
using System;

namespace RateLimiter.Services
{
    public class RateLimiterService: IRateLimiterService
    {
        public RateLimiterService(IDataAccess data)
        {
            this.data = data;
            this.config = GetConfiguration("SomeRule");
        }

        private IDataAccess data;
        private Configuration config;

        public Configuration GetConfiguration(string configurationRule)
        {
            //Create some configurations based on configuration Rules
            var newConfiguration = new Configuration
            {
                MaxActionsInPeriod = 5,
                MinTimeBetweenActions = TimeSpan.FromSeconds(5),
                Period = TimeSpan.FromMinutes(5)
            };

            return newConfiguration;
        }

        public bool IsLimited(RateLimiterRules rule, string accessToken)
        {   
            //Get Configuration 
            var timePeriod = config.Period;
            var maxActionsInPeriod = config.MaxActionsInPeriod;
            var minTimeBetweenActions = config.MinTimeBetweenActions;
            
            // Get Clients Data
            var actionsInPeriod = data.ClientActionsInPeriod(accessToken, timePeriod);
            var lastAction = data.LastClientAction(accessToken); 

            if (RateLimiterRules.All == rule)
            {
                return (IsMaxActionInPeriod(actionsInPeriod, maxActionsInPeriod) || IsTimeBetweenActions(lastAction, minTimeBetweenActions));
            }
            else if (RateLimiterRules.MaximumAcctionsInTimeSpan == rule)
            {
                return IsMaxActionInPeriod(actionsInPeriod, maxActionsInPeriod);
            }
            else if (RateLimiterRules.TimeBetweenActions == rule)
            {
                return IsTimeBetweenActions(lastAction, minTimeBetweenActions);
            }
        
            return false;
        }

        public bool IsMaxActionInPeriod(int actionsInPeriod, int maxActionsInPeriod)
        {
            return (actionsInPeriod >= maxActionsInPeriod);
        }
        public bool IsTimeBetweenActions(DateTime lastAction, TimeSpan? minTimeBetweenActions)
        {
            if (lastAction == null) { return false; }
            if (minTimeBetweenActions == null) { return false; }

            return lastAction.Add((TimeSpan)minTimeBetweenActions) > DateTime.UtcNow;
        }

    }

}
