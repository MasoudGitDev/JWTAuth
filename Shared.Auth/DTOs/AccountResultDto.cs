using Shared.Auth.Enums;
using Shared.Auth.Models;

namespace Shared.Auth.DTOs;

public interface IClientResult {
    public ResultStatus Status { get; init; }
    public List<CodeMessage> Messages { get; }
};

public record AccountResultDto(
       ResultStatus Status ,
       string AuthToken ,
       Dictionary<string , string> KeyValueClaims ,
       List<CodeMessage> Messages) : IClientResult;
