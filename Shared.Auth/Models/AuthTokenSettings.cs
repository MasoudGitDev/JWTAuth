namespace Shared.Auth.Models;
public record AuthTokenSettingsModel(
    string SecretKey ,
    string Issuer ,
    string Audience ,
    double ExpireMinutes = 60);