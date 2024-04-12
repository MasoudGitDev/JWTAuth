namespace Shared.Auth.DTOs;

public record EmailConfirmationDto {
   public string Email { get; set; } = String.Empty;
    public string Token { get; set; } = String.Empty;
}
