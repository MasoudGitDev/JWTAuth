namespace Shared.Auth.Extensions;
public static class StringExtensions {

    public static (string name, bool falg) IsAnyItemNullOrWhitespace(params string[] source) {
        foreach(var item in source) {
            if(String.IsNullOrWhiteSpace(item))
                return (item, true);
        }
        return (String.Empty, false);
    }

}
