using Shared.Auth.ValueObjects;

namespace Apps.Services.Services.Security;

public interface IClaimsGenerator {
    Dictionary<string , string> CreateRegularClaims(AppUserId appUserId , string displayName = "");
    Dictionary<string , string> CreateBlockClaims(AppUserId appUserId , string reason , string displayName = "");
}
