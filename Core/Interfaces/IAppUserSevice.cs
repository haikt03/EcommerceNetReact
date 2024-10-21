using Core.Entities;
using Core.Helpers;
using Core.Utils;

namespace Core.Interfaces
{
    public interface IAppUserSevice
    {
        Task<PagedList<AppUser>> GetAllAsync(PaginationParam paginationParam, string? search = null);
        Task<AppUser> GetByIdAsync(int id);
        Task<AppUser> GetByEmailAsync(string email);
        Task<bool> CreateAsync(AppUser user, string password);
        Task<bool> UpdateAsync(AppUser user);
        Task<bool> RemoveAsync(AppUser user);
        Task<bool> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword);
        Task<bool> SendResetPasswordEmailAsync(AppUser user, string callbackUrl);
        Task<bool> ResetPasswordAsync(AppUser user, string token, string newPassword);
        Task<bool> SendConfirmationEmailAsync(AppUser user, string callbackUrl);
        Task<bool> ConfirmEmailAsync(AppUser user, string token);

    }
}
