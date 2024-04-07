using Shared.Auth.Exceptions;

namespace Apps.Services.Exceptions;
internal class MsgSenderException : CustomException {   
    public MsgSenderException(string description) : base(description) {
        Update("MsgSenderError");
    }

    public MsgSenderException(string code , string description) : base(code , description) {
    }
}
