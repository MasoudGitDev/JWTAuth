namespace Shared.Auth.Enums;
public record ResultStatus(string Name) {
    public static ResultStatus Failed => new("failed");
    public static ResultStatus Succeed => new("succeed");

    public static implicit operator string(ResultStatus status) => status.Name;
}
