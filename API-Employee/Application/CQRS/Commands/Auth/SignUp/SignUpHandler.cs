using Application.Common;
using Application.CQRS.DTOs.Auth;
using Application.Exceptions;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;

namespace Application.CQRS.Commands.Auth.SignUp
{
    public class SignUpHandler : IRequestHandler<SignUpCommand, ApiResult<SignUpDTO>>
    {
        private readonly UserManager<AppUser> _userManager;

        public SignUpHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResult<SignUpDTO>> Handle(
            SignUpCommand request,
            CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLower();

            if (await _userManager.Users.AnyAsync(x => x.Email == email))
                throw new BadRequestException("Email already exists");

            if (await _userManager.Users.AnyAsync(x =>
                x.NormalizedUserName == request.Username.ToUpper()))
                throw new BadRequestException("Username already exists");

            var user = new AppUser
            {
                UserName = request.Username.Trim(),
                NormalizedUserName = request.Username.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new BadRequestException("Register failed");

            await _userManager.AddToRoleAsync(user, UserRoleConst.Member);

            return ApiResult<SignUpDTO>.Success(new SignUpDTO { Ok = true });
        }
    }

}
