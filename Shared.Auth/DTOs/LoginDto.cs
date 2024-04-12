using Shared.Auth.Enums;

namespace Shared.Auth.DTOs;

public record LoginDto {


    public LoginType LoginType { get; set; } = LoginType.UserName;
    public string LoginName { get; set; }
    public string Password { get; set; }
    public bool IsPersistent { get; set; } = false;

    //for search faster
    public DateTime? MembershipDate { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender? Gender { get; set; }

    // for security
    public string? Captcha { get; set; }

    public (
        LoginType LoginType ,
        string LoginName ,
        string Password ,
        bool IsPersistent) GetValues()
        => (LoginType, LoginName, Password, IsPersistent);
}
