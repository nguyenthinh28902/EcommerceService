using AutoMapper;
using EcommerceAuthenticationCore.Models;
using EcommerceAuthenticationService.ViewModels.AdminUserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.AutoMapper
{
    public class AdminUserProfile : Profile
    {
      public AdminUserProfile()
      {
            CreateMap<AdminUser, AdminUserViewModel>();
      }
    }
}
