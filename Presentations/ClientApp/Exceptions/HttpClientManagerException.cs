using Shared.Auth.Exceptions;
using Shared.Auth.Models;

namespace ClientApp.Exceptions;

public class HttpClientManagerException : CustomException {
    public HttpClientManagerException() {
    }

    public HttpClientManagerException(List<CodeMessage> errors) : base(errors) {
    }

    public HttpClientManagerException(string description) : base(description) {
    }

    public HttpClientManagerException(string code , string description) : base(code , description) {
    }
}
