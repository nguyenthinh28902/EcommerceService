using EcommerceAuthenticationCore.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationCore.Default
{
    public class Seed
    {
        public static void SeedData(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceAuthenticationServiceContext>();

            if (!dbContext.AdminUsers.Any())
            {
                dbContext.AdminUsers.AddRange(DataDefault.GetAdminUsers());
                dbContext.SaveChanges();
            }
        }
    }
}
