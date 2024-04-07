using Domains.Auth.AppUserEntity.Aggregate;
using Domains.Auth.AppUserEntity.Repos;
using Microsoft.AspNetCore.Identity;
using Shared.Auth.ValueObjects;

namespace Infra.Auth.Implements.Accounts;
internal sealed class AppUserQueries(UserManager<AppUser> _userManager) : IAppUserQueries {
    public async Task<AppUser?> FindByEmailAsync(string email) {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<AppUser?> FindByIdAsync(AppUserId appUserId) {
        return await _userManager.FindByIdAsync(appUserId.Value.ToString());
    }

    public async Task<AppUser?> FindByUserNameAsync(string userName) {
        return await _userManager.FindByNameAsync(userName);
    }

    public Task<List<AppUser>> GetLockedUsersAsync() {
        throw new NotImplementedException();
    }

    public Task<AppUser?> IsLockedBySystem(AccountLockType lockType) {
        throw new NotImplementedException();
    }
}
