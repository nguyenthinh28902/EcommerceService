using AutoMapper;
using EcommerceAuthenticationData.Interfaces;
using EcommerceAuthenticationService.Helpers;
using EcommerceAuthenticationService.Interfaces;
using EcommerceAuthenticationService.Libraries;
using EcommerceAuthenticationService.ViewModels.AdminUserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.Services
{
    public class AdminService : IAdminService
    {
        private readonly IDalAdminUser _dalAdminUser;
        private readonly IMapper _mapper;
        public AdminService(IDalAdminUser dalAdminUser, IMapper mapper) {
            _dalAdminUser = dalAdminUser;
            _mapper = mapper;
        }
        public async Task<AdminUserViewModel> CheckLoginAsync(AdminUserSignIn adminUserSignIn)
        {
            var user = await _dalAdminUser.GetUserByIdAsync(adminUserSignIn.Id);
            if (user == null ||
                !PasswordHasher.VerifyPassword(adminUserSignIn.Password, user.Password)) throw new AppException("Username or password is incorrect");
            var adminUserViewModel = _mapper.Map<AdminUserViewModel>(user);
            return adminUserViewModel;
        }
    }
}
