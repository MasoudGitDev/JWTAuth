using Shared.Auth.Enums;

namespace AuthWebAPI.DTOs;

public record SignInDto(
    LoginType LoginType,
    string LoginName,
    string Password,
    bool IsPersistent = false) {   

    //for search faster
    public DateTime? MembershipDate { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender? Gender { get; set; }

    // for security
    public string? Captcha { get; set; }


}
