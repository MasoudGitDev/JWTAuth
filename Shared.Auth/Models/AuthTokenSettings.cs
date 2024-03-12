namespace Shared.Auth.Models;
public record AuthTokenSettings(
    string secureKey ,
    string Issuer ,
    string Audience ,
    double ExpireMinutes = 60);