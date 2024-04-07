namespace Domains.Auth.AppUserEntity.ValueObjects;
public class LoginInfo {
    public string MacAddress { get; private set; } = "<undefined>";
    public string BrowserName { get; private set; } = "<undefined>";
    public string? UniqueId { get; set; }
    public bool CanAccess { get; private set; } = true;
}
