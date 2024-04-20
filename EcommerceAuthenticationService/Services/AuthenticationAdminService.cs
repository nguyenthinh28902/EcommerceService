using AutoMapper;
using EcommerceAuthenticationData.Interfaces;
using EcommerceAuthenticationService.Helpers;
using EcommerceAuthenticationService.Interfaces;
using EcommerceAuthenticationService.Libraries;
using EcommerceAuthenticationService.ViewModels.AdminUserViewModels;
using Microsoft.Extensions.Options;

namespace EcommerceAuthenticationService.Services
{
    public class AuthenticationAdminService: IAuthenticationAdminService
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly IDalAdminUser _dalAdminUser;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly IAuthService _authService;
        private readonly JwtSettings _jwtSettings;
        public AuthenticationAdminService(IRedisCacheService redisCacheService, 
            IDalAdminUser dalAdminUser, IMapper mapper, IJwtUtils jwtUtils, IAuthService authService,
            IOptions<JwtSettings> jwtSettings)
        {
            _redisCacheService = redisCacheService;
            _dalAdminUser = dalAdminUser;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
            _authService = authService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task SignOutAsync(string jwt)
        {
            // Thêm JWT vào blacklist với thời gian sống ngắn
            TimeSpan expiry = TimeSpan.FromMinutes(_jwtSettings.ExpireMinutes); // Thời gian sống của token trong blacklist
            await _authService.UpdateTokenLogoutAsync(jwt);
            await _redisCacheService.AddToBlacklistAsync(jwt, expiry);
        }
        public async Task<JwtToken> SignInAsync(AdminUserSignIn adminUserSignIn)
        {
            var user = await _dalAdminUser.GetUserByIdAsync(adminUserSignIn.Id);
            if (user == null ||
                !PasswordHasher.VerifyPassword(adminUserSignIn.Password, user.Password)) throw new AppException("Username or password is incorrect");
            var adminUserViewModel = _mapper.Map<AdminUserViewModel>(user);
            var jwtToken = _jwtUtils.GenerateToken(adminUserViewModel);
            await _authService.SaveTokenLoginAsync(adminUserViewModel.Id, jwtToken.ExpiresAt,jwtToken.Token,nameof(AuthenticationAdminService),nameof(AuthenticationAdminService),true);
            return jwtToken;
        }
    }
}
