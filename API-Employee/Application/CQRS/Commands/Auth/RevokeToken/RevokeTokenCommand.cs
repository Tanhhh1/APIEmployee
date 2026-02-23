using MediatR;

namespace Application.CQRS.Commands.Auth.RevokeToken
{
    public class RevokeTokenCommand : IRequest
    {
        public string RefreshToken { get; set; } = null!;
    }
}
