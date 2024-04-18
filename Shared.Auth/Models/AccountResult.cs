using Shared.Auth.Enums;

namespace Shared.Auth.Models;
public record AccountResult {

    public string AuthToken { get; private set; } = String.Empty;
    public Dictionary<string , string> KeyValueClaims { get; private set; } = [];
    public ResultStatus ResultStatus { get; private set; } = ResultStatus.Failed;
    public List<CodeMessage> Messages { get; private set; } = [];

    private AccountResult() {

    }

    public AccountResult(string authToken , Dictionary<string , string> keyValueClaims) {
        AuthToken = authToken;
        KeyValueClaims = keyValueClaims;
        ResultStatus = ResultStatus.Succeed;
    }

    public AccountResult(
        string authToken ,
        Dictionary<string , string> keyValueClaims ,
        List<CodeMessage> messages) {
        AuthToken = authToken;
        KeyValueClaims = keyValueClaims;
        Messages = messages;
        ResultStatus = ResultStatus.Succeed;
    }

    public AccountResult(List<CodeMessage> errors) {
        Messages = errors;
    }

};