namespace Shared.Auth.Exceptions;
public class NotNullException : CustomException {
    public NotNullException(string description) : base(description) {
    }

    public NotNullException(string code , string description) : base(code , description) {
    }
}
