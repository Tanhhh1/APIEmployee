using Application.Exceptions;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.CQRS.Commands.Auth.RevokeToken
{
    public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand>
    {
        private readonly IUserRefreshTokenRepository _refreshTokenRepo;

        public RevokeTokenHandler(IUserRefreshTokenRepository refreshTokenRepo)
        {
            _refreshTokenRepo = refreshTokenRepo;
        }

        public async Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var token = _refreshTokenRepo
                .GetByCondition(x => x.RefreshToken == request.RefreshToken)
                .FirstOrDefault();

            if (token == null)
                throw new NotFoundException("Refresh token not found");

            token.IsRevoked = true;
            token.UpdatedAt = DateTime.UtcNow;

            _refreshTokenRepo.Update(token);
            await _refreshTokenRepo.SaveChangesAsync();
        }
    }
}
