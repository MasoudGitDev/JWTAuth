using Apps.Auth.Abstractions;
using Jose;
using Shared.Auth.Enums;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using Shared.Auth.ValueObjects;
using System.Text;

namespace Apps.Auth.Services;

internal class JwtService(AuthTokenSettingsModel tokenSettings) : IAuthService {


    private readonly JwsAlgorithm _algorithm  = JwsAlgorithm.HS256;
    private readonly byte[] _securityKey = Encoding.UTF8.GetBytes(tokenSettings.SecretKey);

    // ======================= publics methods
    public static JwtService Create(AuthTokenSettingsModel tokenSetting)
       => new(tokenSetting);

    public async Task<AccountResult> GenerateTokenAsync(Dictionary<string,string> claims) {
        return ( new AccountResult(await EncodeAsync(claims) , claims) );
    }
   
    public async Task<AccountResult> EvaluateAsync(string token , Func<string , Task<AppUserId>> userFinder) {
        var claims = await DecodeAsync(token);
        TokenValidator.Validate(claims);

        string userId = (claims[AuthTokenType.UserId])
            .ThrowIfNullOrWhiteSpace(nameof(userId));

        AppUserId findRealUserId = (await userFinder.Invoke(userId))
            .ThrowIfNull(("AppUserId"));

        return await GenerateTokenAsync(claims); // must be check later
    }

    public async Task<AccountResult> RefreshAsync(string token , Func<string , Task<AppUserId>> userFinder) {
        var result = await EvaluateAsync(token,userFinder);
        var userId = (result.KeyValueClaims[AuthTokenType.UserId])
            .ThrowIfNullOrWhiteSpace("UserId");
        return await GenerateTokenAsync(result.KeyValueClaims);
    }
    // ================= private methods

    private Task<string> EncodeAsync(Dictionary<string , string> claims)
        => Task.FromResult(( JWT.Encode(claims , _securityKey , _algorithm) )
            .ThrowIfNullOrWhiteSpace("System can not generate an <auth-token>."));

    private Task<Dictionary<string , string>> DecodeAsync(string token) {
        var claims = ( JWT.Decode(token , _securityKey , _algorithm) )
            .ThrowIfNullOrWhiteSpace("<Auth-Token>")
            .FromJsonToType<Dictionary<string , string>>()
            .ThrowIfNull("<claims>");
        TokenValidator.Validate(claims);
        return Task.FromResult(claims);
    }
}