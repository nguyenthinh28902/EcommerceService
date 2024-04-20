
using EcommerceAuthenticationAdminApi.Helpers;
using EcommerceAuthenticationService.Interfaces;
using EcommerceAuthenticationService.ViewModels.AdminUserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EcommerceAuthenticationAdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationAdminController : ControllerBase
    {
        private readonly IAuthenticationAdminService _authenticationService;
        private readonly ILogger<AuthorizationAdminController> _logger;
        public AuthorizationAdminController(IAuthenticationAdminService authenticationService, ILogger<AuthorizationAdminController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [Route("v1/sign-in")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] AdminUserSignIn admin)
        {
            var userSignIn = await _authenticationService.SignInAsync(admin);
            return Ok(userSignIn);
        }

        [Route("v1/sign-out")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            var jwt = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await _authenticationService.SignOutAsync(jwt);
            return Ok("Logged out successfully");
        }

        [Route("v1/authentication")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Authentication()
        {
            return Ok(true);
        }

    }
}
