using Shared.Auth.Constants;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;

namespace Apps.Auth.Services.TokenValidationChains;

internal class IssuerValidation(string _issuer) : TokenValidationChain {
    public override void Apply(Dictionary<string , string> claims) {
        base.Apply(claims);
        string issuer = (claims[TokenKey.Issuer])
            .ThrowIfNullOrWhiteSpace("Issuer");
        if(issuer != _issuer) {
            throw new InvalidTokenException("The <Issuer> of the token is invalid.");
        }
    }
}
