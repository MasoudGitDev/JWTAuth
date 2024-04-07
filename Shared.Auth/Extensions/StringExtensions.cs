namespace Shared.Auth.Extensions;
public static class StringExtensions {

    public static (string name, bool falg) IsAnyItemNullOrWhitespace(params string[]? source) {
        if (source is null) {
            return ("Array", true);
        }
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

    public static DateTime AsDateTime(this string source) {
        var canConvert = DateTime.TryParse(source , out DateTime dateTime);
        if(canConvert is false) {
            throw new InvalidCastException("Invalid source to convert to <date-time>");
        }
        return dateTime;
    }


}
