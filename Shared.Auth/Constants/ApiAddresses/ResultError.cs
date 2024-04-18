using Shared.Auth.Models;

namespace Shared.Auth.Constants.ApiAddresses;
public record ResultError(string Code , string Message) {

    public static ResultError InvalidToken => new("InvalidToken" , "The <token> is invalid.");

    public static implicit operator CodeMessage(ResultError error) => new(error.Code, error.Message);

}
