using Shared.Auth.Enums;

namespace Shared.Auth.DTOs;

public record SignUpDto {

    //[EmailAddress]
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public Gender? Gender { get; set; } = null;
    public DateTime? BirthDate { get; set; } = null;

    //[Compare(nameof(Password))]
    public string ConfirmedPassword { get; set; } = String.Empty;

    public string Captcha { get; set; } = String.Empty;

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
