using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Utils;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class AppUserService : IAppUserSevice
    {
        private readonly UserManager<AppUser> _userManager;
        public AppUserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<PagedList<AppUser>> GetAllAsync(PaginationParam paginationParam, string? search = null)
        {
            var query = _userManager.Users.AsQueryable().Search(search).OrderBy(a => a.UserName);
            var result = await query.ToPagedListAsync(paginationParam.PageSize, paginationParam.PageIndex);

            return result;
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            return (await _userManager.FindByIdAsync(id.ToString()))!;
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            return (await _userManager.FindByEmailAsync(email))!;
        }

        public async Task<bool> CreateAsync(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> UpdateAsync(AppUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> RemoveAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.Succeeded;
        }
    }
}
