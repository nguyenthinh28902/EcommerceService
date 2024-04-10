using EcommerceAuthenticationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationCore.Default
{
    public static class DataDefault
    {
        public static List<AdminUser> GetAdminUsers()
        {
            var adminUsers = new List<AdminUser>();
            var role = new Role()
            {
                Name = "administrator",
                DisplayName = "Quản trị viên",
                IsDeleted = false,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdateAt = DateTimeOffset.UtcNow,
            };
            adminUsers.Add(new AdminUser()
            {
                Name = "administrator",
                DisplayName = "Quản trị viên",
                Avatar = "",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdateAt = DateTimeOffset.UtcNow,
                PhoneNumber = "0359342009",
                EmailAddres = "thinh48691953@gmail.com",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                IsDeleted = false,
                Roles = new List<Role>() { role }
            });
            return adminUsers;
        }
    }
}
