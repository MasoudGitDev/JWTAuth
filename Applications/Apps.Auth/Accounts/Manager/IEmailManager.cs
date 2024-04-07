using Domains.Auth.AppUserEntity.Aggregate;
using Shared.Auth.Models;

namespace Apps.Auth.Accounts.Manager;
public interface IEmailManager {
    Task<AccountResult> ChangeEmailAsync(AppUser appUser , string newEmail , string token);
    Task<AccountResult> RequestChangeEmailAsync(AppUser appUser , string newEmail , LinkModel model);
    Task<AccountResult> ConfirmEmailAsync(AppUser appUser , string confirmationToken);
    Task<AccountResult> ResendEmailConformationLink(AppUser appUser , LinkModel model);
}
