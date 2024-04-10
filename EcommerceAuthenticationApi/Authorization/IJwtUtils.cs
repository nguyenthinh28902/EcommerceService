using EcommerceAuthenticationCore.Models;
using EcommerceAuthenticationService.ViewModels.AdminUserViewModels;

namespace EcommerceAuthenticationApi.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateToken(AdminUserViewModel user);
        public int? ValidateToken(string token);
    }
}
