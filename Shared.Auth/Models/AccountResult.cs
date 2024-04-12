using Shared.Auth.Abstractions;
using Shared.Auth.Enums;

namespace Shared.Auth.Models;
public record AccountResult{

    public string AuthToken { get; private set; } = String.Empty;
    public Dictionary<string , string> KeyValueClaims { get; private set; } = [];
    public ResultStatus ResultStatus { get; private set; } = ResultStatus.Failed;
    public List<CodeMessage> Errors { get; private set; } = [];

    private AccountResult()
    {
       
    }

    public AccountResult(string authToken , Dictionary<string , string> keyValueClaims) {
        AuthToken = authToken;
        KeyValueClaims = keyValueClaims;
        Errors = [];
        ResultStatus = ResultStatus.Succeed;
    }

    public AccountResult(
        string authToken ,
        Dictionary<string , string> keyValueClaims ,
        List<CodeMessage> errors) {
        AuthToken = authToken;
        KeyValueClaims = keyValueClaims;
        Errors = errors;
        ResultStatus = ResultStatus.Failed;
    }

    public AccountResult(List<CodeMessage> errors){
        Errors = errors;
    }

};

public record ErrorResult(string[] Errors) : IRequestResult;

public record CodeMessage(string Code , string Message);

