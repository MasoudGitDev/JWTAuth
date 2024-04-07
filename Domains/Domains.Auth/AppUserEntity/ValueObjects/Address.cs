using Domains.Auth.Shared.Abstractions;
using Domains.Auth.Shared.Events;
using Shared.Auth.Extensions;
namespace Domains.Auth.AppUserEntity.ValueObjects;
public class Address {
    public string Country { get; private set; } = "Iran";
    public string City { get; private set; } = "Shiraz";
    public string Description { get; private set; } = "<undefined>";

    private Address() { }

    public static Address Create(string country , string city , string description) {
        var (name, flag) = StringExtensions.IsAnyItemNullOrWhitespace(country , city , description);
        if(flag) {
            throw new ArgumentNullException(nameof(name));
        }
        return new() { Country = country , City = name , Description = description };
    }

    public static Address Create(Address address) {
        return Create(address.Country , address.City , address.Description);
    }

    public void ChangeCountry(string country , Action<IDomainEvent> eventAction) {
        var oldData  = Country;
        Country = country;
        eventAction.Invoke(new ChangeEvent<string>("Address.Country" , oldData , country));
    }
    public void ChangeCity(string city , Action<IDomainEvent> eventAction) {
        var oldData = City;
        City = city;
        eventAction.Invoke(new ChangeEvent<string>("Address.City" , oldData , city));
    }
    public void ChangeDescription(string description , Action<IDomainEvent> eventAction) {
        var oldData = Description;
        Description = description;
        eventAction.Invoke(new ChangeEvent<string>("Address.Description" , oldData , description));
    }

    public static implicit operator Address?(string jsonSource) => jsonSource.FromJsonToType<Address>();
    public static implicit operator string?(Address address) => address.ToJson();

}
