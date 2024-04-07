namespace AuthWebAPI.DTOs;

public record ChangePasswordDto(string CurrentPassword , string NewPassword , string ConfirmNewPassword);
