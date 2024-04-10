using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.Helpers
{
    public class AppException : Exception
    {

        public AppException(string message) : base()
        {
        }
        public AppException(string message, params object[] args)
            : base(string.Format(System.Globalization.CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
