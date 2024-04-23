namespace Shared.Auth.DTOs;

public record LoginDto {

    /// <summary>
    /// LoginName Can be Email or UserName.
    /// </summary>
    public string LoginName { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public bool IsPersistent { get; set; } = false;    

    //Enable these later for search faster
    //public DateTime? MembershipDate { get; set; }
    //public DateTime? BirthDate { get; set; }
    //public Gender? Gender { get; set; }

    public (string LoginName, string Password, bool IsPersistent) GetValues()
        => (LoginName, Password, IsPersistent);
}
