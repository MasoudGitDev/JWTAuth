using Domains.Auth.AppUserEntity.Aggregate;
using Shared.Auth.Models;

namespace Apps.Auth.Accounts.Manager;

public interface IPasswordManager {
    Task ForgotAsync(AppUser appUser , LinkModel linkModel);
    Task<AccountResult> ResetAsync(AppUser appUser , string token , string newPassword);
    Task<AccountResult> ChangeAsync(AppUser appUser , string currentPassword , string newPassword);
}
