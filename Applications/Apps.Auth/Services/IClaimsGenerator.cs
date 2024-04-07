using Shared.Auth.ValueObjects;

namespace Apps.Auth.Services;

public interface IClaimsGenerator {
    Dictionary<string , string> GetSignUpClaims(AppUserId appUserId);
    Dictionary<string , string> CreateRegularClaims(AppUserId appUserId);
    Dictionary<string , string> CreateBlockClaims(AppUserId appUserId , string reason);
}
