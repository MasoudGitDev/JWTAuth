﻿using Shared.Auth.Enums;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;

namespace Apps.Auth.Services.TokenValidationChains;

internal class AudiencesValidation(string[] _audiences) : TokenValidationChain {
    public override void Apply(Dictionary<string , string> claims) {
        base.Apply(claims);
        string audience = (claims[AuthTokenType.Audience])
            .ThrowIfNullOrWhiteSpace("Audience");
        if(!_audiences.Contains(audience)) {
            throw new InvalidTokenException("The <Audience> of token is invalid.");
        }
    }
}