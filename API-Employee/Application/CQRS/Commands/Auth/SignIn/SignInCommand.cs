using Application.Common;
using Application.CQRS.DTOs.Auth;
using MediatR;

namespace Application.CQRS.Commands.Auth.SignIn
{
    public class SignInCommand : IRequest<ApiResult<SignInDTO>>
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
