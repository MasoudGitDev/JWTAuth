using Apps.Services.Services.AccountManagers;
using Domains.Auth.AppUserEntity.Repos;
using Shared.Auth.Abstractions;

namespace Apps.Services.Services;
public interface IAccountUOW {

    IAppUserQueries Queries { get; }

    IMessageSender MessageSender { get; }

    IPasswordManager PasswordManager { get; }
    IEmailManager EmailManager { get; }
    IAccountManager AccountManager { get; }

    Task SaveChangesAsync();
    void Delete<TEntity>(TEntity entity) where TEntity : IEntity;
}
