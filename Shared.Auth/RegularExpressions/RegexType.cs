using System.Text.RegularExpressions;

namespace Shared.Auth.RegularExpressions;
public static class RegexType {
    public static Regex Email => new(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
}
