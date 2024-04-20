using EcommerceAuthenticationApi.Authorization;
using EcommerceAuthenticationApi.Helpers;
using EcommerceAuthenticationService.Interfaces;
using EcommerceAuthenticationService.ViewModels.AdminUserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceAuthenticationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IAuthenticationAdminService _authenticationService;
        private readonly IJwtUtils _jwtUtils;
        public AuthorizationController(IAdminService adminService, IJwtUtils jwtUtils, IRedisCacheService redisCacheService, IAuthenticationAdminService authenticationService)
        {
            _adminService = adminService;
            _jwtUtils = jwtUtils;
            _redisCacheService = redisCacheService;
            _authenticationService = authenticationService;
        }

        [Route("v1/admin/sign-in")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] AdminUserSignIn admin)
        {
            var userSignIn = await _adminService.CheckLoginAsync(admin);
            var token = _jwtUtils.GenerateToken(userSignIn);
            var jwtToken = new JwtToken();
            jwtToken.Token = token;
            return Ok(jwtToken);
        }

        [Route("v1/admin/sign-out")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            var jwt = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            // Thêm JWT vào blacklist với thời gian sống ngắn
            await _authenticationService.SignOutAsync(jwt);
            return Ok("Logged out successfully");
        }

        [Route("v1/admin/authentication")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Authentication()
        {
            return Ok(true);
        }

    }
}
