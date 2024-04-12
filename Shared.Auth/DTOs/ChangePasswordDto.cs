namespace Shared.Auth.DTOs;

public record ChangePasswordDto{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmNewPassword { get; set; } = string.Empty;

    public static ChangePasswordDto Empty => new();
}

