using Shared.Auth.Enums;
using Shared.Auth.Models;

namespace Shared.Auth.DTOs;

public record Result: IClientResult {
    public ResultStatus Status { get; private set; } = ResultStatus.Failed;
    public List<CodeMessage> Messages { get; private set; } = [];

    public Result(ResultStatus status , List<CodeMessage> messages)
    {
        Status = status;
        Messages = messages;
    }
}
