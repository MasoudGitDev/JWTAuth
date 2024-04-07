using Apps.Auth.Abstractions;
using Jose;
using Microsoft.IdentityModel.Tokens;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using Shared.Auth.ValueObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Apps.Auth.Services;

internal class DefaultJwtService(AuthTokenSettingsModel _tokenSettings) : IAuthService {

    private readonly byte[] _securityKey = Encoding.UTF8.GetBytes(_tokenSettings.SecretKey);

    // ======================= publics methods
    public static JwtService Create(AuthTokenSettingsModel tokenSetting)
       => new(tokenSetting);

    public Task<AccountResult> EvaluateAsync(string token , Func<string , Task<AppUserId>> userFinder) {
        var handler = new JwtSecurityTokenHandler();
        bool canReadToken = handler.CanReadToken(token);
        if(canReadToken) {
            throw new InvalidTokenException("InvalidToken");
        }
        var securityToken = handler.ReadJwtToken(token);
        var claims = (securityToken.Claims.ToDictionary(x=>x.Type, x => x.Value))
            .ThrowIfEmpty("Invalid claims.");
        return Task.FromResult(new AccountResult(token , claims));
    }

    public Task<AccountResult> GenerateTokenAsync(Dictionary<string , string> claims) {
        var userClaims = new List<Claim>();
        Parallel.ForEach(claims , item => {
            userClaims.Add(new Claim(item.Key , item.Value));
        });
        var tokenDescriptor = new SecurityTokenDescriptor(){
            Audience = _tokenSettings.Audience,
            Issuer = _tokenSettings.Issuer,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpireMinutes),
            Subject = new ClaimsIdentity(userClaims),
            SigningCredentials =new SigningCredentials(
               new SymmetricSecurityKey(_securityKey) ,
               SecurityAlgorithms.HmacSha256Signature) ,
        };
        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.CreateToken(tokenDescriptor);
        return Task.FromResult(new AccountResult(handler.WriteToken(securityToken) , claims));
    }



}