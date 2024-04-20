using EcommerceAuthenticationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcommerceAuthenticationApi.Helpers
{
    public class BlacklistAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            var redisCacheService = context.HttpContext.RequestServices.GetRequiredService<RedisCacheService>();

            var authorizeAttribute = context.Filters.OfType<AuthorizeAttribute>().FirstOrDefault();
            if (authorizeAttribute == null)
            {
                // Nếu không tìm thấy [Authorize], không cần kiểm tra blacklist
                return;
            }

            var authResult = await authService.AuthorizeAsync(context.HttpContext.User, null, authorizeAttribute.Policy);
            if (!authResult.Succeeded)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }

            // Kiểm tra blacklist
            string jwt = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(jwt) && await redisCacheService.IsBlacklistedAsync(jwt))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }
        }
    }
}
