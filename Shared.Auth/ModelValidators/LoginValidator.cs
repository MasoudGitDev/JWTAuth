using FluentValidation;
using Shared.Auth.DTOs;
using Shared.Auth.Extensions;

namespace Shared.Auth.ModelValidators;
public class LoginValidator : AbstractValidator<LoginDto> {

    public LoginValidator()
    {
        CheckLoginName();
        CheckPasswords();
    }

    private void CheckLoginName() {
        RuleFor(x=>x.LoginName).Must(x=> !String.IsNullOrWhiteSpace(x))
            .WithMessage($"The <login-name> can not be null or whiteSpace.");
    }

    private void CheckPasswords() {
        string propertyName = "Password";
        RuleFor(x => x.Password).BePasswordChars(propertyName);
    }


}
