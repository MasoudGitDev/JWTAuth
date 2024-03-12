using Domains.Auth.AppUserEntity.Aggregate;

namespace Apps.Auth.Managers;
internal abstract partial class AccountManager<TModel, TReturn> {

    protected async Task<AppUser?> FindByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

    protected async Task<AppUser?> FindByUserName(string userName)
        => await _userManager.FindByNameAsync(userName);

}
