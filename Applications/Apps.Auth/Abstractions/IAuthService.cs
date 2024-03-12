using Shared.Auth.Models;
using Shared.Auth.ValueObjects;

namespace Apps.Auth.Abstractions;
public interface IAuthService {
    Task<AccountResult> GenerateTokenAsync(AppUserId appUserId);
    Task<AccountResult> EvaluateAsync(string token);
}