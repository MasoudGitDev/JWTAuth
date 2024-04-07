using Shared.Auth.Enums;
using Shared.Auth.Exceptions;

namespace Apps.Auth.Services.TokenValidationChains;

internal abstract class TokenValidationChain
{


    public virtual void Apply(Dictionary<string, string> claims)
    {
      //  ShouldMatch([.. claims.Keys] , ClaimKeys);
    }

    private void ShouldMatch(string[] source, string[] items)
    {
        if (items.Length != source.Length)
            throw new InvalidTokenException("The lengths of items must be same.");

        int count = 0;
        foreach (var item in items)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                break;
            }
            if (source.Contains(item))
            {
                count++;
            }
        }
        if (count != source.Length)
        {
            throw new InvalidTokenException("The <Claims-key> has been not matched.");
        }

    }

    private string[] ClaimKeys => [
        AuthTokenType.Id ,
        AuthTokenType.UserId ,
        AuthTokenType.IsBlocked ,
        AuthTokenType.Issuer ,
        AuthTokenType.IssuerAt ,
        AuthTokenType.Audience ,
        AuthTokenType.ExpireAt
   ];
}
