using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.ViewModels.AdminUserViewModels
{
    public class AdminUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddres { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdateAt { get; set; }
    }
}
