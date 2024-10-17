using Core.Entities;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(AppUser user);
        Task<RefreshToken> GenerateRefreshToken(AppUser user);
        Task<bool> ValidateRefreshToken(AppUser user, string refreshToken);
    }
}
