using EcommerceAuthenticationService.AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService.DependencyInjection
{
    public static class DependencyInjectionAutoMapper
    {
        public static IServiceCollection ServiceAutoMapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AdminUserProfile).Assembly);
            return services;
        }
    }
}
