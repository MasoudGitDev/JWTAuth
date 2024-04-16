using FluentValidation;
using Shared.Auth.DTOs;
using Shared.Auth.Extensions;


namespace Shared.Auth.ModelValidators;

public class SignUpValidator : AbstractValidator<SignUpDto> {

    public SignUpValidator() {
        CheckEmail();
        CheckUserName();
        CheckPasswords();

    }

    private void CheckPasswords() {
        string propertyName = "Password";
        RuleFor(x => x.Password).BePasswordChars(propertyName);
        RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password)
            .WithMessage("The <Password> do not match with <ConfirmPassword>.");
    }

    private void CheckUserName() {
        string propertyName = "UserName";
        RuleFor(x => x.UserName).BeUserNameChars(propertyName);
    }


    private void CheckEmail() =>
        RuleFor(x => x.Email)
        .EmailAddress()
        .WithMessage("Please enter a valid email address.");



}
