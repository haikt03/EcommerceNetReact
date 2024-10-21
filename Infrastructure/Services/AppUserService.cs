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
        private readonly IEmailService _emailService;
        public AppUserService(UserManager<AppUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<PagedList<AppUser>> GetAllAsync(PaginationParam paginationParam, string? search = null)
        {
            var query = _userManager.Users.AsQueryable().Search(search).OrderBy(a => a.UserName);
            var result = await query.ToPagedListAsync(paginationParam.PageSize, paginationParam.PageIndex);

            return result;
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new NullReferenceException("User not found");
            return user;
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NullReferenceException("User not found");
            return user;
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

        public async Task<bool> SendResetPasswordEmailAsync(AppUser user, string callbackUrl)
        {
            if (user.Email == null)
                throw new ArgumentNullException("Email not found");

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"{callbackUrl}?token={Uri.EscapeDataString(resetToken)}&email={Uri.EscapeDataString(user.Email)}";

            var emailSubject = "Yêu cầu đặt lại mật khẩu";
            var emailBody = $"Để đặt lại mật khẩu của bạn hãy bấm vào <a href=\"{resetLink}\">đây</a>.";
            await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(AppUser user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }

        public async Task<bool> SendConfirmationEmailAsync(AppUser user, string callbackUrl)
        {
            if (user.Email == null)
                throw new ArgumentNullException("Email not found");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{callbackUrl}?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email)}";

            var emailSubject = "Confirm your email";
            var emailBody = $"Để xác nhận email của bạn hãy bấm vào <a href=\"{confirmationLink}\">đây</a>.";

            await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);

            return true;
        }

        public async Task<bool> ConfirmEmailAsync(AppUser user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }
    }
}
