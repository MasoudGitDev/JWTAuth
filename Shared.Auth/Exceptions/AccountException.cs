using Shared.Auth.Models;

namespace Shared.Auth.Exceptions;
public class AccountException : CustomException {
    
    public AccountException(string code , string description) : base(code , description) {
    }

    public AccountException(List<CodeMessage> errors) : base(errors) { }
}
