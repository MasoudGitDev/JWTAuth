namespace Shared.Auth.Exceptions;
public abstract class CustomException :Exception {

    public string Code { get; private set; } = "<not-defined>";
    public string Description { get; private set; } = "<not-defined>";

    public CustomException() : base() { }

    public CustomException(string code , string description) :base(description)
    {
        Code = code; 
        Description = description;
    }
    public CustomException(string description) : base(description) {
        Description = description;
    }

    public void Update(string code) {
        this.Code = code;
    }

}
