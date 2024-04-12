using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Auth.Extensions;
public static class JsonExtensions {

    public static JsonSerializerOptions JsonSerializerOptions 
        => new() {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = false ,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase ,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
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

    public static T? FromJsonToType<T>(this string? source) {
        if(source is null) {
            return default;
        }
        return JsonSerializer.Deserialize<T>(source , JsonSerializerOptions);
    }

    public static StringContent AsStringContent<T>(this T data)
        => new(data.ToJson() , System.Text.Encoding.UTF8 , "application/json");

    
}
