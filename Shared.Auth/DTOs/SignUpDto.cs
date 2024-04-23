using Shared.Auth.Enums;

namespace Shared.Auth.DTOs;

public record SignUpDto {

    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public Gender Gender { get; set; } = Gender.Male;

    // the default value in utcNow because of faster-search to find the user.
    public DateTime BirthDate { get; set; } = DateTime.UtcNow;

    public string ConfirmedPassword { get; set; } = String.Empty;

    public static SignUpDto Empty => new();
    public (
        string Email,
        string UserName,
        string Password,
        Gender? Gender,
        DateTime? BirthDate
        ) GetValues()
        => (Email, UserName, Password, Gender, BirthDate);

}
