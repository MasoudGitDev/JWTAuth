using Shared.Auth.Models;
using Shared.Auth.ValueObjects;

namespace Apps.Auth.Abstractions;
public interface IAuthService {
    Task<AccountResult> GenerateTokenAsync(
        Dictionary<string , string> claims ,
        List<CodeMessage>? errors = default);

    Task<AccountResult> EvaluateAsync(
        string token ,
        Func<string , Task<AppUserId>> userFinder);
}