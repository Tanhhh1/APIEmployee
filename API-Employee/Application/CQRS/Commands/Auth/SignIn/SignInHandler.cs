using Application.Common;
using Application.CQRS.DTOs.Auth;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Logger;


namespace Application.CQRS.Commands.Auth.SignIn
{
    public class SignInHandler : IRequestHandler<SignInCommand, ApiResult<SignInDTO>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUserRefreshTokenRepository _refreshTokenRepo;

        public SignInHandler(UserManager<AppUser> userManager, ITokenService tokenService, IUserRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _refreshTokenRepo = refreshTokenRepository;
        }

        public async Task<ApiResult<SignInDTO>> Handle(
            SignInCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                throw new UnauthorizedException("Invalid credentials");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
                throw new UnauthorizedException("Invalid credentials");

            var tokenResult = await _tokenService.GenerateAsync(user);

            var refreshTokenEntity = new UserRefreshToken
            {
                UserId = user.Id.ToString(),
                AccessToken = tokenResult.AccessToken,
                RefreshToken = tokenResult.RefreshToken,
                ExpiryTime = tokenResult.Expires,
                IsUsed = false,
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepo.AddAsync(refreshTokenEntity);
            await _refreshTokenRepo.SaveChangesAsync();

            return ApiResult<SignInDTO>.Success(new SignInDTO 
            {
                AccessToken = tokenResult.AccessToken,
                RefreshToken = tokenResult.RefreshToken,
                Expires = tokenResult.Expires
            });
        }
    }
}
