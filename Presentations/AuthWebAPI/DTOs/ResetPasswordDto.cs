namespace AuthWebAPI.DTOs;

public record ResetPasswordDto(string Token , string Email , string Password , string ConfirmPassword);
