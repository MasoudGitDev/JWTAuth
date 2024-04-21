using Shared.Auth.Models;

namespace Shared.Auth.Constants.ApiAddresses;

public record ResultMessage(string Code , string Message) {
    public static ResultMessage InvalidToken => new("InvalidToken" , "The <token> is invalid.");
    public static ResultMessage InvalidCaptcha => new("InvalidCaptcha" , "The <captcha> is invalid.");

    public static ResultMessage ValidCaptcha => new("ValidCaptcha" , "The <captcha> is validated successfully.");

    public static implicit operator CodeMessage(ResultMessage model) => new(model.Code , model.Message);

}
