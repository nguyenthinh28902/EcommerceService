using EcommerceAuthenticationService.Helpers;
using EcommerceAuthenticationService.ViewModels.AdminUserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.Interfaces
{
    public interface IAuthenticationAdminService
    {
        public Task SignOutAsync(string jwt);
        public Task<JwtToken> SignInAsync(AdminUserSignIn adminUserSignIn);
    }
}
