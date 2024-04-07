namespace Shared.Auth.Exceptions;
public class InvalidTokenException : CustomException {    
    public InvalidTokenException(string description) : base(description) {
        Update("InvalidToken");
    }

    public InvalidTokenException(string code , string description ="The Token is invalid.")
        : base(code , description) {
    }
}
