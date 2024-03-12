namespace Shared.Auth.Exceptions;
public class EmptyListException : CustomException {
    public EmptyListException(string description) : base(description) {
        Update("Empty");
    }
}
