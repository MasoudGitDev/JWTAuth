using Apps.Services.Services.Security;
using Shared.Auth.Constants;
using Shared.Auth.Models;
using Shared.Auth.ValueObjects;

namespace Apps.Services.Implementations.Jwt;

public sealed class ClaimsGenerator(AuthTokenSettingsModel tokenSettings) : IClaimsGenerator
{


    public Dictionary<string, string> CreateRegularClaims(AppUserId appUserId, string displayName = "")
    {
        var claims = Shared(appUserId, displayName);
        claims.Add(TokenKey.IsBlocked, false.ToString());
        claims.Add(TokenKey.Reason, "OK");
        return claims;
    }
    public Dictionary<string, string> CreateBlockClaims(AppUserId appUserId, string reason, string displayName = "")
    {
        var claims = Shared(appUserId, displayName);
        claims.Add(TokenKey.IsBlocked, true.ToString());
        claims.Add(TokenKey.Reason, reason);
        return claims;
    }


    // ========================== privates
    private Dictionary<string, string> Shared(AppUserId appUserId, string displayName)
     => new() {
            { TokenKey.DisplayName , displayName},
            { TokenKey.Id , Guid.NewGuid().ToString() },
            { TokenKey.UserId , appUserId.Value.ToString() } ,
            { TokenKey.IssuerAt , DateTime.UtcNow.ToString() } ,
            { TokenKey.Issuer , tokenSettings.Issuer } ,
            { TokenKey.Audience , tokenSettings.Audience } ,
            { TokenKey.ExpireAt , DateTime.UtcNow.AddMinutes(tokenSettings.ExpireMinutes).ToString() },
     };

}
