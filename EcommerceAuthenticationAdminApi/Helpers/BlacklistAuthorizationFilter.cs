using EcommerceAuthenticationService.Interfaces;
using EcommerceAuthenticationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcommerceAuthenticationAdminApi.Helpers
{
    public class BlacklistAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            var redisCacheService = context.HttpContext.RequestServices.GetRequiredService<IRedisCacheService>();

            var hasAuthorizeAttribute = context.ActionDescriptor.EndpointMetadata
            .Any(em => em.GetType() == typeof(AuthorizeAttribute));

        if (!hasAuthorizeAttribute)
        {
            return;
        }

        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
        }
            string jwt = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(jwt) && await redisCacheService.IsBlacklistedAsync(jwt))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }
        }
    }
}
