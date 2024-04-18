using Shared.Auth.Constants;
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
        TokenKey.Id ,
        TokenKey.UserId ,
        TokenKey.IsBlocked ,
        TokenKey.Issuer ,
        TokenKey.IssuerAt ,
        TokenKey.Audience ,
        TokenKey.ExpireAt
   ];
}
