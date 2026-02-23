using Application.CQRS.DTOs.Auth;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared.Helpers;
using Shared.Models;

namespace Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSetting _jwtSetting;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(
            IOptions<JwtSetting> jwtSetting,
            UserManager<AppUser> userManager)
        {
            _jwtSetting = jwtSetting.Value;
            _userManager = userManager;
        }

        public async Task<SignInDTO> GenerateAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var jwtUser = new JwtUserInformation
            {
                Id = user.Id,
                Name = user.UserName,
                UniqueName = user.UserName,
                Email = user.Email
            };

            var issuedAt = DateTime.UtcNow;

            var accessToken = JwtHelper.GenerateToken(
                roles,
                _jwtSetting,
                jwtUser,
                issuedAt
            );

            return new SignInDTO
            {
                AccessToken = accessToken!,
                RefreshToken = StringHelper.RandomString(64),
                Expires = issuedAt.AddMinutes(_jwtSetting.TokenValidityInMinutes)
            };
        }
    }
}
