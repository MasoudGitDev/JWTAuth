using Shared.Auth.Abstractions;
using Shared.Auth.ValueObjects;

namespace Shared.Auth.Models;
public record AppUserResult(AppUserId AppUserId , string UserName , string Email):IRequestResult;
