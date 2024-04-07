using Shared.Auth.Enums;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;

namespace Apps.Auth.Services.TokenValidationChains;

internal class IsBlockedValidation : TokenValidationChain {
    public override void Apply(Dictionary<string , string> claims) {
        base.Apply(claims);
        string isBlocked = (claims[AuthTokenType.IsBlocked])
            .ThrowIfNullOrWhiteSpace("IsBlocked");
        if(isBlocked.Equals("true" , StringComparison.CurrentCultureIgnoreCase)) {
            throw new InvalidTokenException("The Token has been <Blocked>.");
        }
    }
}