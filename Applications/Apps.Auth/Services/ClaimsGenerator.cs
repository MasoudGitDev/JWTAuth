using Shared.Auth.Enums;
using Shared.Auth.Models;
using Shared.Auth.ValueObjects;

namespace Apps.Auth.Services;

public sealed class ClaimsGenerator(AuthTokenSettingsModel tokenSettings) : IClaimsGenerator {

    public Dictionary<string , string> GetSignUpClaims(AppUserId appUserId) {
        var claims = Shared(appUserId);
        claims.Add(AuthTokenType.IsBlocked , true.ToString());
        claims.Add(AuthTokenType.IsEmailConfirmed , false.ToString());
        return claims;
    }

    public Dictionary<string , string> CreateRegularClaims(AppUserId appUserId) {
        var claims = Shared(appUserId);
        claims.Add(AuthTokenType.IsBlocked , false.ToString());
        return claims;
    }
    public Dictionary<string , string> CreateBlockClaims(AppUserId appUserId , string reason) {
        var claims = Shared(appUserId);
        claims.Add(AuthTokenType.IsBlocked , true.ToString());
        claims.Add(AuthTokenType.Reason , reason);
        return claims;
    }


    // ========================== privates
    private Dictionary<string , string> Shared(AppUserId appUserId)
     => new() {
            { AuthTokenType.Id , Guid.NewGuid().ToString() },
            { AuthTokenType.UserId , appUserId.Value.ToString() } ,
            { AuthTokenType.IssuerAt , DateTime.UtcNow.ToString() } ,
            { AuthTokenType.Issuer , tokenSettings.Issuer } ,
            { AuthTokenType.Audience , tokenSettings.Audience } ,
            { AuthTokenType.ExpireAt , DateTime.UtcNow.AddMinutes(tokenSettings.ExpireMinutes).ToString() },
     };

}
