using System.Text.Json;

namespace Shared.Auth.Extensions;
public static class JsonExtensions {

    public static JsonSerializerOptions JsonSerializerOptions 
        => new JsonSerializerOptions() {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = false ,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

    public static string ToJson<T>(this T? source) { 
        if(source is null) {
            return string.Empty;
        }
        if(source is string && String.IsNullOrWhiteSpace(source as string)) { 
            return string.Empty; 
        }
        return JsonSerializer.Serialize<T>(source,JsonSerializerOptions);
    }

    public static T? FromJsonString<T>(this string? source) {
        return source is null ?
            default : JsonSerializer.Deserialize<T>(source , JsonSerializerOptions);
    }

}
