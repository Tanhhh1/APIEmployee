using Application.CQRS.DTOs.Auth;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.Commands.Auth.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenDTO>
    {
        private readonly IUserRefreshTokenRepository _refreshTokenRepo;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;

        public RefreshTokenHandler(IUserRefreshTokenRepository refreshTokenRepo, ITokenService tokenService, UserManager<AppUser> userManager)
        {
            _refreshTokenRepo = refreshTokenRepo;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<RefreshTokenDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var storedToken = _refreshTokenRepo
                .GetByCondition(x =>
                    x.AccessToken == request.AccessToken &&
                    x.RefreshToken == request.RefreshToken &&
                    !x.IsRevoked &&
                    !x.IsUsed
                )
                .FirstOrDefault();

            if (storedToken == null)
                throw new UnauthorizedException("Invalid refresh token");

            if (storedToken.ExpiryTime < DateTime.UtcNow)
                throw new UnauthorizedException("Refresh token expired");

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            if (user == null)
                throw new UnauthorizedException("User not found");

            storedToken.IsUsed = true;
            storedToken.UpdatedAt = DateTime.UtcNow;
            _refreshTokenRepo.Update(storedToken);

            var newToken = await _tokenService.GenerateAsync(user);

            var newRefreshToken = new UserRefreshToken
            {
                UserId = user.Id.ToString(),
                AccessToken = newToken.AccessToken,
                RefreshToken = newToken.RefreshToken,
                ExpiryTime = newToken.Expires,
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepo.AddAsync(newRefreshToken);
            await _refreshTokenRepo.SaveChangesAsync();

            return new RefreshTokenDTO
            {
                AccessToken = newToken.AccessToken,
                RefreshToken = newToken.RefreshToken,
                Expires = newToken.Expires
            };
        }
    }
}
