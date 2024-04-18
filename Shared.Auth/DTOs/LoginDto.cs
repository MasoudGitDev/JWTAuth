namespace Shared.Auth.DTOs;

public record LoginDto {

    /// <summary>
    /// LoginName Can be Email or UserName.
    /// </summary>
    public string LoginName { get; set; }
    public string Password { get; set; }
    public bool IsPersistent { get; set; } = false;

    // for security
    public string Captcha { get; set; } = String.Empty;

    //Enable these later for search faster
    //public DateTime? MembershipDate { get; set; }
    //public DateTime? BirthDate { get; set; }
    //public Gender? Gender { get; set; }

    public (string LoginName, string Password, bool IsPersistent , string Captcha) GetValues()
        => (LoginName, Password, IsPersistent, Captcha);
}
