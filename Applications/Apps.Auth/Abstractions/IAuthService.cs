using Shared.Auth.Models;
using Shared.Auth.ValueObjects;

namespace Apps.Auth.Abstractions;
public interface IAuthTokenService {
    Task<AccountResult> GenerateAsync(
        Dictionary<string , string> claims ,
        List<CodeMessage>? errors = default);

    Task<AccountResult> EvaluateAsync(
        string token ,
        Func<string , Task<AppUserId>> userFinder);
}