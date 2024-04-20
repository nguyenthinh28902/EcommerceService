using EcommerceAuthenticationService.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _redis;

        public RedisCacheService(IConnectionMultiplexer muxer)
        {
            _redis = muxer.GetDatabase();
        }
        public async Task AddToBlacklistAsync(string jwt, TimeSpan expiry)
        {
            await _redis.StringSetAsync(jwt, "invalid", expiry);
        }

        public async Task<bool> IsBlacklistedAsync(string jwt)
        {
            return await _redis.KeyExistsAsync(jwt);
        }
    }
}
