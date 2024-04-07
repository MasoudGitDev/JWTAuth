using Shared.Auth.Abstractions;
using Shared.Auth.Enums;

namespace Shared.Auth.Models;
public record AccountResult(
    string AuthToken ,
    Dictionary<string , string> KeyValueClaims) : IRequestResult {

    public ResultStatus ResultStatus { get; private set; }
    public AccountResult(
        ResultStatus resultStatus ,
        string authToken ,
        Dictionary<string , string> claims) : this(authToken , claims) {
        ResultStatus = resultStatus;
    }

};

public record ErrorResult(string[] Errors) : IRequestResult;

public record Result(string Code , string Message);
