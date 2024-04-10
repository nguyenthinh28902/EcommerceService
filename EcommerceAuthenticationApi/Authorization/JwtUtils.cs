using EcommerceAuthenticationCore.Models;
using EcommerceAuthenticationService.Helpers;
using EcommerceAuthenticationService.ViewModels.AdminUserViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceAuthenticationApi.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private readonly JwtSettings _appSettings;

        public JwtUtils(IOptions<JwtSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public string GenerateToken(AdminUserViewModel user)
        {
            // generate token that is valid for 7 days

            var Claims = new List<Claim>();
            Claims.Add(new Claim("id", user.Id.ToString()));
            Claims.Add(new Claim(ClaimTypes.Name, user.Name.ToString()));
            Claims.Add(new Claim(ClaimTypes.GivenName, user.DisplayName.ToString()));
            Claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber.ToString()));
            Claims.Add(new Claim(ClaimTypes.Email, user.EmailAddres.ToString()));
            Claims.Add(new Claim("avatar", user.Avatar.ToString()));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Aud, _appSettings.Aud));

            var ExpiryDuration = new TimeSpan(0, _appSettings.ExpireMinutes, 0);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(_appSettings.Issuer, _appSettings.Issuer, Claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
