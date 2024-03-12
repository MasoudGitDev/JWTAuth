using Shared.Auth.Abstractions;
using Shared.Auth.Enums;

namespace Shared.Auth.Models;
public record AccountResult(
    ResultStatus ResultStatus ,
    string AuthToken ,
    Dictionary<string , string> KeyValueClaims) : IRequestResult;
