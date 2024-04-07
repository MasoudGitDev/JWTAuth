using Domains.Auth.AppUserEntity.Aggregate;
using Shared.Auth.Models;

namespace Apps.Auth.Accounts.Manager;

public interface IPasswordManager {
    Task ForgotPasswordAsync(AppUser appUser , LinkModel linkModel);
    Task<AccountResult> ResetPasswordAsync(AppUser appUser , string token , string newPassword);
    Task<AccountResult> ChangePasswordAsync(AppUser appUser , string currentPassword , string newPassword);
}
