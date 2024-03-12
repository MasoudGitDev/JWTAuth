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
    public async Task<AccountResult> GenerateTokenAsync(AppUserId appUserId) {
        var claims = GenerateClaims(appUserId);
        return ( new AccountResult(ResultStatus.Succeed , await EncodeAsync(claims) , claims) );
    }
    public async Task<AccountResult> EvaluateAsync(string token)
       => new AccountResult(ResultStatus.Succeed , token , await DecodeAsync(token));

    private Dictionary<string , string> GenerateClaims(AppUserId appUserId)
        => new() {
            { AuthTokenType.Id , Guid.NewGuid().ToString() },
            { AuthTokenType.UserId , appUserId } ,
            { AuthTokenType.IssuerAt , DateTime.UtcNow.ToString() } ,
            { AuthTokenType.Issuer , tokenSettings.Issuer } ,
            { AuthTokenType.Audience , tokenSettings.Audience } ,
            { AuthTokenType.Expire , DateTime.UtcNow.AddMinutes(tokenSettings.ExpireMinutes).ToString() } ,
        };

    // ================= private methods

    private Task<string> EncodeAsync(Dictionary<string , string> claims)
        => Task.FromResult(( JWT.Encode(claims , _securityKey , _algorithm) )
            .ThrowIfNullOrWhiteSpace("System can not generate an <auth-token>."));

    private Task<Dictionary<string , string>> DecodeAsync(string token) {
        return Task.FromResult(( JWT.Decode(token , _securityKey , _algorithm) )
            .ThrowIfNullOrWhiteSpace("<Auth-Token>")
            .FromJsonToType<Dictionary<string , string>>()
            .ThrowIfNull("<claims>")
            .ThrowIfEmpty("<claims>"));
    }
}
