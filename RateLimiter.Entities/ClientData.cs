using System;

namespace RateLimiter.Entities
{
    public class ClientData
    {
        public string AccessToken { get; set; }        
        public DateTime AccessTime { get; set; }
    }
}
