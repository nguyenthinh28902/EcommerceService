using EcommerceAuthenticationCore.Models;
using EcommerceAuthenticationService.ViewModels.AdminUserViewModels;

namespace EcommerceAuthenticationService.Helpers
{
    public interface IJwtUtils
    {
        public JwtToken GenerateToken(AdminUserViewModel user);
        public int? ValidateToken(string token);
    }
}
