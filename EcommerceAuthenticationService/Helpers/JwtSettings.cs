using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.Helpers
{
    public class JwtSettings
    {
        public string Aud { get; set; }
        public int ExpireMinutes { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
    }
}
