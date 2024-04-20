using EcommerceAuthenticationCore.Default;
using EcommerceAuthenticationCore.Models;
using EcommerceAuthenticationCore.Repositories;
using EcommerceAuthenticationData.Interfaces;
using EcommerceAuthenticationData.Repositories;
using EcommerceAuthenticationData.Services;
using EcommerceAuthenticationService.AutoMapper;
using EcommerceAuthenticationService.Helpers;
using EcommerceAuthenticationService.Interfaces;
using EcommerceAuthenticationService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace EcommerceAuthenticationService.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection ServiceDescriptors(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration["RedisOption:ConnectionString"]));
            services.AddSingleton<IJwtUtils, JwtUtils>();
            services.AddDbContext<EcommerceAuthenticationServiceContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDalAdminUser, DalAdminUser>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IDalUserToken, DalUserToken>();


            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthenticationAdminService, AuthenticationAdminService>();
           

            return services;
        }

        public static IServiceProvider ServiceProvider(this IServiceProvider serviceProvider)
        {
            Seed.SeedData(serviceProvider);
            return serviceProvider;
        }

    }
}
