using Apps.Auth.Abstractions;
using Apps.Auth.Accounts.Manager;
using Apps.Services.Services;
using Domains.Auth.AppUserEntity.Repos;
using Infra.Auth.Contexts.Write;
using Shared.Auth.Abstractions;

namespace Infra.Auth.Implements.Accounts;
internal sealed class AccountUOW(
    IAppUserQueries _queries ,
    IPasswordManager _passwordManager ,
    IAccountManager _accountManager ,
    IEmailManager _emailManager ,    
    AppWriteDbContext _dbContext
    ) : IAccountUOW {

    public IAppUserQueries Queries => _queries;

    public IMessageSender MessageSender => throw new NotImplementedException();

    public IEmailManager EmailManager => _emailManager;
    public IPasswordManager PasswordManager => _passwordManager;
    public IAccountManager AccountManager => _accountManager;
    public void Delete<TEntity>(TEntity entity) where TEntity : IEntity {
        _dbContext.Remove(entity);
    }

    public async Task SaveChangesAsync() {
        await _dbContext.SaveChangesAsync(cancellationToken: new CancellationToken());
    }



}
