using Application.CQRS.DTOs.Auth;
using MediatR;

namespace Application.CQRS.Commands.Auth.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenDTO>
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
