namespace Shared.Auth.DTOs;
public record ForgotPasswordDto {
    public string Email { get; set; } = String.Empty;
}
