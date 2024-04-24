using Shared.Auth.Constants;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;

namespace Apps.Services.Implementations.Jwt.TokenValidationChains;

internal class AudiencesValidation(string[] _audiences) : TokenValidationChain {
    public override void Apply(Dictionary<string , string> claims) {
        base.Apply(claims);
        string audience = claims[TokenKey.Audience]
            .ThrowIfNullOrWhiteSpace("Audience");
        if(!_audiences.Contains(audience)) {
            throw new InvalidTokenException("The <Audience> of token is invalid.");
        }
    }
}