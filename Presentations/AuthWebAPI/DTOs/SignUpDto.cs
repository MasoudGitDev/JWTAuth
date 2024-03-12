using Shared.Auth.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuthWebAPI.DTOs;

public class SignUpDto {
    required public string Email { get; set; }
    required public string UserName { get; set; }
    required public DateTime BirthDate { get; set; } = DateTime.UtcNow;
    required public Gender Gender { get; set; } = Gender.Male;

    required public string Password { get; set; }

    [Compare(nameof(Password))]
    required public string ConfirmedPassword { get; set; }

    public string? Captcha { get; set; }


}
