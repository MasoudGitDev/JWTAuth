using Shared.Auth.Exceptions;

namespace Apps.Services.Exceptions;
internal class SmtpException : CustomException {   
    public SmtpException(string description) : base(description) {
        Update("SmtpError");
    }

    public SmtpException(string code , string description) : base(code , description) {
    }
}
