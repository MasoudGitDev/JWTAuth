namespace Shared.Auth.Exceptions;
public class InvalidGuidException : CustomException {
    public InvalidGuidException(string description = "The Guid value can not be all zeros.") : base(description) {
        this.Update("Invalid-Guid");
    }

    public InvalidGuidException(string code , string description) : base(code , description) {
    }
}
