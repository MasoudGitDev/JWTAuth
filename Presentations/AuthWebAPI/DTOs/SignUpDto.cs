using Shared.Auth.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AuthWebAPI.DTOs;

public record SignUpDto(
    [EmailAddress] string Email,    
    string UserName,
    string Password ,
    Gender Gender = Gender.Male ,
    DateTime? BirthDate = null) {

    [Compare(nameof(Password))]
    required public string ConfirmedPassword { get; set; }

    public string? Captcha { get; set; }

    
}
