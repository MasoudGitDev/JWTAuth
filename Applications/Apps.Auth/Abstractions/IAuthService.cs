using Shared.Auth.Models;
using Shared.Auth.ValueObjects;

namespace Apps.Auth.Abstractions;
public interface IAuthService {
    //Task<AccountResult> GenerateTokenAsync(AppUserId appUserId , bool isBlocked = false);
    Task<AccountResult> GenerateTokenAsync(Dictionary<string , string> claims);
    Task<AccountResult> EvaluateAsync(string token , Func<string , Task<AppUserId>> userFinder);
}