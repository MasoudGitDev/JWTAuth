using Shared.Auth.Enums;
using Shared.Auth.Models;

namespace Shared.Auth.DTOs;

public record Result(ResultStatus Status , List<CodeMessage> Messages) : IClientResult;
