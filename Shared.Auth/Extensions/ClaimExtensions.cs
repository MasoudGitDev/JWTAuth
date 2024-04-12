using System.Security.Claims;

namespace Shared.Auth.Extensions;
public static class ClaimExtensions {



    public static ClaimsIdentity AsClaimsIdentity(this Dictionary<string , string> claims) {
        var claimList = new List<Claim>();
        Parallel.ForEach(claims , item => {
            claimList.Add(new Claim(item.Key, item.Value));
        });
        return new(claimList);
    }


}
