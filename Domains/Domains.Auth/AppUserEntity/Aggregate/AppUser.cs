using Domains.Auth.AppUserEntity.ValueObjects;
using Domains.Auth.Shared.Abstractions;
using Shared.Auth.Enums;
using System.Runtime.CompilerServices;


[assembly:InternalsVisibleTo("Apps.Auth")]
namespace Domains.Auth.AppUserEntity.Aggregate;

internal partial class AppUser : AppUserAggregateRoot {

    public bool IsLockedBySystem { get; private set; } = false;
    public bool IsLockedByOwner { get; private set; } = false;

    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }

    public Address? Address { get; private set; }
    public LoginInfo LoginInfo { get; private set; }

}
