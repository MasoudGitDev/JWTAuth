namespace Shared.Auth.Constants.ApiAddresses;
public record AccountAction(string Name)
{

    private static string BaseAddress(string name) => "Api/Account/" + name;

    public static AccountAction Register => new(BaseAddress("Register"));
    public static AccountAction Login => new(BaseAddress("Login"));
    public static AccountAction LoginByToken => new(BaseAddress("LoginByToken"));
    public static AccountAction Delete => new(BaseAddress("Delete"));

    public override string ToString()
    {
        return Name;
    }
    public static implicit operator string(AccountAction accountAction) => accountAction.Name;
}
