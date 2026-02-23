using Application.Common;
using Application.CQRS.DTOs.Auth;
using MediatR;

namespace Application.CQRS.Commands.Auth.SignUp
{
    public class SignUpCommand : IRequest<ApiResult<SignUpDTO>>
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

}
