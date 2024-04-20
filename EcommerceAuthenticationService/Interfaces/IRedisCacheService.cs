using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.Interfaces
{
    public interface IRedisCacheService
    {
        public Task AddToBlacklistAsync(string jwt, TimeSpan expiry);
        public Task<bool> IsBlacklistedAsync(string jwt);
    }
}
