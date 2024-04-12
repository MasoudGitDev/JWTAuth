namespace Shared.Auth.Constants.ApiAddresses;

public record PasswordAction(string Name) {
    private static string BaseAddress(string name) => "Api/Password/" + name;

    public static AccountAction Change => new(BaseAddress("Change"));
    public static AccountAction Forgot => new(BaseAddress("Forgot"));
    public static AccountAction Reset => new(BaseAddress("Reset"));

    public override string ToString() {
        return Name;
    }
    public static implicit operator string(PasswordAction accountAction) => accountAction.Name;
}

