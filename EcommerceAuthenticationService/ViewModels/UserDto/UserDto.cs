using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.ViewModels.UserDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public bool IsAdmin { get; set; }
    }
}
