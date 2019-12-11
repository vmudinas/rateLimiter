using System;

namespace RateLimiter.Entities
{
    public class Configuration
    {
        public TimeSpan Period { get; set; }
        public int MaxActionsInPeriod { get; set; }
        public TimeSpan? MinTimeBetweenActions { get; set; }
    }
}
