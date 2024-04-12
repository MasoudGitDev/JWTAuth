namespace Shared.Auth.DTOs;

public record ResetPasswordDto{
    public string Token { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string ConfirmPassword { get; set; } = String.Empty;

    public static ResetPasswordDto Empty => new();
};
