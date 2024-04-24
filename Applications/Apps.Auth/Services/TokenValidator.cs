using Apps.Auth.Services.TokenValidationChains;

namespace Apps.Auth.Services;

internal class TokenValidator() {

    private static SortedList<byte , TokenValidationChain> Validators => new() {
         // { 0 , new ItemCountsValidation(7) } ,
         //  { 1 , new IsBlockedValidation() },      
        { 2 , new DateTimeValidation(60) },
        { 3 , new IssuerValidation("https://localhost:7224") },
        { 4 , new AudiencesValidation(["https://localhost:7224"]) }
    };

    public static bool Validate(Dictionary<string , string> _claims) {
        foreach(var validator in Validators) {
            validator.Value.Apply(_claims);
        }
        return true;
    }
}
