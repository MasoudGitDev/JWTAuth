namespace Shared.Auth.Exceptions;
public class AccountsException : CustomException {
    
    public AccountsException(string code , string description) : base(code , description) {
    }

    public AccountsException(Dictionary<string, string> errors) : base(errors) { }
}
