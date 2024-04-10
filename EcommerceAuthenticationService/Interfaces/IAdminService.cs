using EcommerceAuthenticationService.ViewModels.AdminUserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.Interfaces
{
    public interface IAdminService
    {
        Task<AdminUserViewModel> CheckLoginAsync(AdminUserSignIn adminUserSignIn);
    }
}
