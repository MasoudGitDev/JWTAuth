using Shared.Auth.Enums;
using Shared.Auth.Models;
using Shared.Auth.ValueObjects;
using System.Security.Claims;

namespace Apps.Auth.Services;

public sealed class ClaimsGenerator(AuthTokenSettingsModel tokenSettings) : IClaimsGenerator {


    public Dictionary<string , string> CreateRegularClaims(AppUserId appUserId , string displayName = "") {
        var claims = Shared(appUserId,displayName);
        claims.Add(AuthTokenType.IsBlocked , false.ToString());
        claims.Add(AuthTokenType.Reason , "Ok");
        return claims;
    }
    public Dictionary<string , string> CreateBlockClaims(AppUserId appUserId , string reason, string displayName = "") {
        var claims = Shared(appUserId,displayName);
        claims.Add(AuthTokenType.IsBlocked , true.ToString());
        claims.Add(AuthTokenType.Reason , reason);
        return claims;
    }


    // ========================== privates
    private Dictionary<string , string> Shared(AppUserId appUserId , string displayName)
     => new() {
            { AuthTokenType.DisplayName , displayName},
            { AuthTokenType.Id , Guid.NewGuid().ToString() },
            { AuthTokenType.UserId , appUserId.Value.ToString() } ,
            { AuthTokenType.IssuerAt , DateTime.UtcNow.ToString() } ,
            { AuthTokenType.Issuer , tokenSettings.Issuer } ,
            { AuthTokenType.Audience , tokenSettings.Audience } ,
            { AuthTokenType.ExpireAt , DateTime.UtcNow.AddMinutes(tokenSettings.ExpireMinutes).ToString() },
     };

}
