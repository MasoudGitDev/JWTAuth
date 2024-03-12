namespace Shared.Auth.Extensions;
public static class StringExtensions {

    public static (string name, bool falg) IsAnyItemNullOrWhitespace(params string[] source) {
        foreach(var item in source) {
            if(String.IsNullOrWhiteSpace(item))
                return (item, true);
        }
        return (String.Empty, false);
    }

    public static string ThrowIfNullOrWhiteSpace(this string? source , string propertyName) { 
        if(string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException($"The <{propertyName}> can not be <NullOrWhiteSpace>.");
        return source;
    }

}
