using Domains.Auth.AppUserEntity.ValueObjects;
using Domains.Auth.Shared.Events;
using Shared.Auth.Enums;
using Shared.Auth.Extensions;
using Shared.Auth.ValueObjects;

namespace Domains.Auth.AppUserEntity.Aggregate;
public partial class AppUser {

    public static AppUser Create(
        string userName ,
        string email ,
        Gender gender = Gender.Male ,
        DateTime? birthDate = null) {
        DateTime currentDT = DateTime.UtcNow;
        return new() {
            Id = Guid.NewGuid() ,
            Gender = gender ,
            BirthDate = CheckDateTime(birthDate) ,
            UserName = userName ,
            Email = email ,
            SystemLock = LockInfo.Create(startAt :currentDT) ,
            CreatedAt = currentDT ,
            OwnerLock = null,
            LoginInfo = new()
        };
    }

    public static AppUser Create(
        AppUserId appUserId ,
        Gender gender = Gender.Male ,
        DateTime? birthDate = null) => new() {
            Id = appUserId ,
            Gender = gender ,
            BirthDate = CheckDateTime(birthDate)
        };

    public void Update(DateTime birthDate) {
        var oldData = BirthDate;
        BirthDate = birthDate;
        RaiseEvent(new ChangeEvent<DateTime>(nameof(BirthDate) , oldData , birthDate));
    }
    public void Update(Gender gender) {
        var oldData = Gender;
        Gender = gender;
        RaiseEvent(new ChangeEvent<Gender>(nameof(Gender) , oldData , gender));
    }


    private static DateTime CheckDateTime(DateTime? dateTime) {
        if(dateTime is null) {
            return DateTime.UtcNow;
        }
        return (DateTime) dateTime;
    }
}

// For Address
public partial class AppUser {
    public void CreateAddress(Address address) {
        var oldData = Address;
        Address.Create(address);
        RaiseEvent(new ChangeEvent<Address?>(nameof(Gender) , oldData , address));
    }

    public void UpdateCountry(string country) {
        Address.ThrowIfNull(nameof(Address)).ChangeCountry(country , RaiseEvent);
    }
    public void UpdateCity(string city) {
        Address.ThrowIfNull(nameof(Address)).ChangeCity(city , RaiseEvent);
    }
    public void ChangeAddressDescription(string description) {
        Address.ThrowIfNull(nameof(Address)).ChangeDescription(description , RaiseEvent);
    }
}

//SystemLock
public partial class AppUser {
    public void SetSystemLock(LockInfo? lockInfo) {
        var oldData = SystemLock;
        SystemLock = lockInfo;
        RaiseEvent(ChangeEvent<LockInfo?>.Set(nameof(SystemLock) , oldData , lockInfo));
    }

    public void SetOwnerLock(LockInfo lockInfo) {
        var oldData = OwnerLock;
        OwnerLock = lockInfo;
        RaiseEvent(ChangeEvent<LockInfo?>.Set(nameof(OwnerLock) , oldData , lockInfo));
    }
}