using Shared.Auth.Enums;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;

namespace Apps.Auth.Services.TokenValidationChains;
internal class DateTimeValidation(double _expireMinute = 60) : TokenValidationChain {
    public override void Apply(Dictionary<string , string> claims) {
        base.Apply(claims);
        DateTime issuerAt = claims[AuthTokenType.IssuerAt].AsDateTime();
        DateTime expireAt = claims[AuthTokenType.ExpireAt].AsDateTime();
        if(expireAt <= DateTime.UtcNow) {
            throw new InvalidTokenException("The Token has been <Expired>.");
        }
        if(issuerAt >= expireAt) {
            throw new InvalidTokenException("The Token has invalid <date-times>.");
        }
        if(expireAt.Subtract(issuerAt).TotalMinutes != _expireMinute) {
            throw new InvalidTokenException("The Token has invalid <date-times>.");
        }
    }
}
