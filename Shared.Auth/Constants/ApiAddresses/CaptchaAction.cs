namespace Shared.Auth.Constants.ApiAddresses;

public record CaptchaAction(string Name) {

    private static string BaseAddress(string name) => "Api/Captcha/" + name;

    public static AccountAction Generate => new(BaseAddress("Generate"));
    public static AccountAction Validate => new(BaseAddress("Validate"));

    public override string ToString() {
        return Name;
    }
    public static implicit operator string(CaptchaAction accountAction) => accountAction.Name;
}
