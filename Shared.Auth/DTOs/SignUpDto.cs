using Shared.Auth.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.Auth.DTOs;

public record SignUpDto {

    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public Gender Gender { get; set; } = Gender.Male;

    // the default value in utcNow because of faster-search to find the user.
    public DateTime? BirthDate { get; set; } = DateTime.UtcNow; 

    public string ConfirmedPassword { get; set; }

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
