using Shared.Auth.Models;

namespace Shared.Auth.Exceptions;
public abstract class CustomException :Exception {

    public string Code { get; private set; } = "<not-defined>";
    public string Description { get; private set; } = "<not-defined>";
    public List<CodeMessage> Errors { get; private set; } = [];

    public CustomException() : base() { }

    public CustomException(string code , string description) :base(description)
    {
        Code = code; 
        Description = description;
        Errors.Add(new(code, description));
    }

    protected CustomException(List<CodeMessage> errors)
    {
        Errors = errors;
    }
    public CustomException(string description) : base(description) {
        Description = description;
        Errors.Add (new(Code, description));
    }

    public void Update(string code) {
        this.Code = code;
    }

}
