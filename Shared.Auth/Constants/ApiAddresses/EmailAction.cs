namespace Shared.Auth.Constants.ApiAddresses;

public record ServerEmailAction(string Name) {
    private static string BaseAddress(string name) => "Api/Email/" + name;

    public static AccountAction ChangeEmail => new(BaseAddress("ChangeEmail"));
    public static AccountAction Confirm => new(BaseAddress("Confirm"));
    public static AccountAction ResendCode => new(BaseAddress("ResendEmailConformationLink"));

    public override string ToString() {
        return Name;
    }
    public static implicit operator string(ServerEmailAction accountAction) => accountAction.Name;
}