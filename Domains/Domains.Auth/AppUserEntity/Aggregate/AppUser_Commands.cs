using Domains.Auth.AppUserEntity.Events;
using Domains.Auth.AppUserEntity.ValueObjects;
using Domains.Auth.Shared.ValueObjects;
using Shared.Auth.Enums;
using Shared.Auth.Extensions;

namespace Domains.Auth.AppUserEntity.Aggregate;
internal partial class AppUser {

    public static AppUser Create(
        Gender gender = Gender.Male ,
        DateTime? birthDate = null) => new() {
            Id = EntityId.Create() ,
            Gender = gender ,
            BirthDate = CheckDateTime(birthDate)
        };

    public void SetSystemLock(bool isLockedBySystem) {
        var oldData = IsLockedBySystem;
        IsLockedBySystem = isLockedBySystem;
        RaiseEvent(new ChangeEvent<bool>(nameof(IsLockedBySystem) , oldData , isLockedBySystem));
    }
    public void SetOwnerLock(bool isLockedByOwner) {
        var oldData = IsLockedByOwner;
        IsLockedByOwner = isLockedByOwner;
        RaiseEvent(new ChangeEvent<bool>(nameof(IsLockedByOwner) , oldData , isLockedByOwner));
    }

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


    private static DateTime CheckDateTime(DateTime? dateTime) {
        if(dateTime is null) {
            return DateTime.UtcNow;
        }
        return (DateTime) dateTime;
    }
}