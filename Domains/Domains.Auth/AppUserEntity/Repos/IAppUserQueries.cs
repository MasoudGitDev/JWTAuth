using Domains.Auth.AppUserEntity.Aggregate;
using Shared.Auth.ValueObjects;

namespace Domains.Auth.AppUserEntity.Repos;
public interface IAppUserQueries {

    Task<AppUser?> FindByIdAsync(AppUserId appUserId);
    Task<AppUser?> FindByEmailAsync(string email);
    Task<AppUser?> FindByUserNameAsync(string userName);

    Task<List<AppUser>> GetLockedUsersAsync();
    Task<AppUser?> IsLockedBySystem(AccountLockType lockType);
}