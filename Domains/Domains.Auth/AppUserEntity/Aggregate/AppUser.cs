using Domains.Auth.AppUserEntity.ValueObjects;
using Domains.Auth.Shared.Abstractions;
using Shared.Auth.Abstractions;
using Shared.Auth.Enums;



namespace Domains.Auth.AppUserEntity.Aggregate;
public partial class AppUser : AppUserAggregateRoot , IEntity , IRequestResult {

    public LockInfo? SystemLock { get; private set; }
    public LockInfo? OwnerLock { get; private set; } = null;

    public DateTime CreatedAt { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }

    public Address? Address { get; private set; }
    public LoginInfo LoginInfo { get; private set; }

}
