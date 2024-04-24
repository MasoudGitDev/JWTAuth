using Domains.Auth.AppUserEntity.Aggregate;
using Shared.Auth.Models;

namespace Apps.Auth.Abstractions.Managers;
public interface IAccountManager
{
    Task<AccountResult> LoginByTokenAsync(string authToken);

    Task<AccountResult> LoginAsync(string loginName, string password, bool isPersistent, bool lockoutOnFailure);

    Task<AccountResult> RegisterAsync(AppUser appUserModel, string password, LinkModel model);
    Task DeleteAsync(AppUser appUser);
}
