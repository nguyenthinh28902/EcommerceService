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
        private readonly IJwtUtils _jwtUtils;
        public AuthorizationController(IAdminService adminService, IJwtUtils jwtUtils) {
            _adminService = adminService;
            _jwtUtils = jwtUtils;
        }

        [Route("v1/SignIn")]
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

        [Route("v1/authentication")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Authentication()
        {
            return Ok(true);
        }

    }
}
