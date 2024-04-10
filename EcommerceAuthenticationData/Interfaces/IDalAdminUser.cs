using EcommerceAuthenticationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationData.Interfaces
{
    public interface IDalAdminUser
    {
        public Task<AdminUser> CreateAsync(AdminUser user);
        public Task<AdminUser> UpdateAsync(AdminUser user);
        public Task<bool> DeleteleAsync(object id);

        public Task<AdminUser?> GetUserByIdAsync(object id);
    }
}
