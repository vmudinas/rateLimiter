using RateLimiter.Services.Contracts;
using RateLimiter.Services;
using RateLimiter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
    public class RateLimiter
    {
    public RateLimiter(IDataAccess dataAccess)
    {
        this.RateService = new RateLimiterService(dataAccess);
    }
    private RateLimiterService RateService { get; set; }
    public bool IsLimited(RateLimiterRules rule, string accessToken)
    {         
        return this.RateService.IsLimited(rule, accessToken);
    }
    }
}
