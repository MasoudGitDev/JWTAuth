using Shared.Auth.Exceptions;

namespace Apps.Auth.Services.TokenValidationChains;

internal class ItemCountsValidation(byte _count) : TokenValidationChain {
    public override void Apply(Dictionary<string , string> claims) {
        base.Apply(claims);
        if(claims.Count != _count) {
            throw new InvalidTokenException("The <Length> of token is invalid.");
        }
    }
}
