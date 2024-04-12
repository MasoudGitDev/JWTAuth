using Shared.Auth.ValueObjects;

namespace Apps.Auth.Services;

public interface IClaimsGenerator {
    Dictionary<string , string> CreateRegularClaims(AppUserId appUserId,string displayName = "");
    Dictionary<string , string> CreateBlockClaims(AppUserId appUserId , string reason, string displayName = "");
}
