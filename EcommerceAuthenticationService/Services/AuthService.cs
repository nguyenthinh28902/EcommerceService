using AutoMapper;
using EcommerceAuthenticationCore.Models;
using EcommerceAuthenticationData.Interfaces;
using EcommerceAuthenticationService.ViewModels.UserDto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using EcommerceAuthenticationService.Interfaces;
namespace EcommerceAuthenticationService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDalUserToken _dalUserToken;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public AuthService(IDalUserToken dalUserToken, IMapper mapper, IHttpContextAccessor accessor)
        {
            _dalUserToken = dalUserToken;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task SaveTokenLoginAsync(int userId , DateTimeOffset ExpiresAt, string token, string appName, string loginProvider, bool IsAdmin)
        {
           await Task.Run(async () =>
            {
                var user = new UserToken();
                user.UserId = userId;
                user.ExpiresAt = ExpiresAt;
                user.Token = token;
                user.IsAdmin = IsAdmin;
                user.AppName = appName;
                user.LoginProvider = loginProvider;
                await _dalUserToken.CreateAsync(user);
            });
           
        }

        public async Task UpdateTokenLogoutAsync(string jwt)
        {
            await Task.Run(async () =>
            {
                var userToken = await _dalUserToken.GetFirstOrDefaultAsync(x => x.Token == jwt);
                if (userToken is not null)
                {
                    userToken.ExpiresAt = DateTimeOffset.UtcNow;
                    await _dalUserToken.UpdateAsync(userToken);
                }
            });

        }

        public UserDto UserClaim()
        {
            var userDto = new UserDto();
            if (int.TryParse(_accessor.HttpContext.User.FindFirst(u => u.Type == "id")?.Value, out int id))
                userDto.Id = id;
            userDto.Name = _accessor.HttpContext.User.FindFirst(u => u.Type == ClaimTypes.Name)?.Value ?? string.Empty;
            userDto.DisplayName = _accessor.HttpContext.User.FindFirst(u => u.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
            userDto.MobilePhone = _accessor.HttpContext.User.FindFirst(u => u.Type == ClaimTypes.MobilePhone)?.Value ?? string.Empty;
            userDto.Email = _accessor.HttpContext.User.FindFirst(u => u.Type == ClaimTypes.Email)?.Value ?? string.Empty;
            userDto.Avatar = _accessor.HttpContext.User.FindFirst(u => u.Type == "avatar")?.Value ?? string.Empty;

            return userDto;
        }
    }
}
