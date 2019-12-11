using RateLimiter.Entities;
using System;

namespace RateLimiter.Services.Contracts
{
    public interface IRateLimiterService
    {
        Configuration GetConfiguration(string configurationRule);
        bool IsLimited(RateLimiterRules rule, string accessToken);

    }
}