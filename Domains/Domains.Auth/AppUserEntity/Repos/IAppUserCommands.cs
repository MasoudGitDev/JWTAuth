using Domains.Auth.AppUserEntity.Aggregate;
using Shared.Auth.Models;

namespace Domains.Auth.AppUserEntity.Repos;

/// <summary>
/// ActionLink : (userId,token) => result
/// </summary>
public interface IAppUserCommands {

    Task<AccountResult> SignUpAsync(AppUser appUser , string password , LinkModel model);

    Task<AccountResult> SignInAsync(AppUser appUser , string password , bool isPersistent , bool lockoutOnFailure);
    Task<AccountResult> LoginByTokenAsync(string authToken);

}


