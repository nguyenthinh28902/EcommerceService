using EcommerceAuthenticationCore.Default;
using EcommerceAuthenticationCore.Models;
using EcommerceAuthenticationCore.Repositories;
using EcommerceAuthenticationData.Interfaces;
using EcommerceAuthenticationData.Repositories;
using EcommerceAuthenticationData.Services;
using EcommerceAuthenticationService.AutoMapper;
using EcommerceAuthenticationService.Interfaces;
using EcommerceAuthenticationService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationService
{
    public static class DependencyInjection
    {
        public static IServiceCollection ServiceDescriptors(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<EcommerceAuthenticationServiceContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDalAdminUser, DalAdminUser>();
            services.AddScoped<IAdminService, AdminService>();

          
            return services;
        }

        public static IServiceProvider ServiceProvider(this IServiceProvider serviceProvider)
        {
            Seed.SeedData(serviceProvider);
            return serviceProvider;
        }
        public static IServiceCollection ServiceAutoMapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AdminUserProfile).Assembly);
            return services;
        }
    }
}
