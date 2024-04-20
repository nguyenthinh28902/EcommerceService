using EcommerceAuthenticationService.ViewModels.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.Interfaces
{
    public interface IAuthService
    {
        public Task SaveTokenLoginAsync(int userId, DateTimeOffset ExpiresAt, string token, string appName, string loginProvider, bool IsAdmin);
        public Task UpdateTokenLogoutAsync(string jwt);
        public UserDto UserClaim();
    }
}
