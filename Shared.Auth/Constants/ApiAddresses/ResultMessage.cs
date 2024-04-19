using Shared.Auth.Models;

namespace Shared.Auth.Constants.ApiAddresses;

public record ResultMessage(string Code , string Message) {

    public static ResultMessage ValidCaptcha => new("ValidCaptcha" , "The <captcha> is validated successfully.");

    public static implicit operator CodeMessage(ResultMessage model) => new(model.Code , model.Message);

}
