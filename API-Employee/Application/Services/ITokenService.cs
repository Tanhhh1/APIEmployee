using Application.CQRS.DTOs.Auth;
using Domain.Identity;

namespace Application.Services
{
    public interface ITokenService
    {
        Task<SignInDTO> GenerateAsync(AppUser user);
    }
}
