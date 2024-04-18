using Shared.Auth.Enums;
using Shared.Auth.Models;

namespace Shared.Auth.DTOs;

public interface IClientResult {
    public ResultStatus Status { get; } 
    public List<CodeMessage> Messages { get; }
};

public record AccountResultDto: IClientResult {
    public string AuthToken { get; private set; } = string.Empty;
    public ResultStatus Status { get; private set; } = ResultStatus.Failed;
    public List<CodeMessage> Messages { get; private set; } = [];    
    public Dictionary<string , string> KeyValueClaims { get; private set; } = [];

    private AccountResultDto()
    {
        
    }

    public AccountResultDto(ResultStatus status ,
        string authToken  ,
        Dictionary<string , string> keyValueClaims ,
        List<CodeMessage> messages)
    {
        Status = status;
        AuthToken = authToken;
        KeyValueClaims = keyValueClaims;
        Messages = messages;        
    }

};
