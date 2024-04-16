using FluentValidation;
using FluentValidation.Results;
using Shared.Auth.Models;

namespace Shared.Auth.Extensions;

public static class CustomValidationExtensions {

    public static IRuleBuilderOptions<TModel , string> BeInRange<TModel>(
      this IRuleBuilder<TModel , string> ruleBuilder,string propertyName , int min = 7 , int max = 80)
      => ruleBuilder
          .NotNullOrWhiteSpace(propertyName)
          .MinimumLength(min).WithMessage($"The <{propertyName}> must have at least {min} characters.")
          .MaximumLength(max).WithMessage($"The <{propertyName}> must have at most {max} characters.");


    public static IRuleBuilderOptions<TModel , string> NotNullOrWhiteSpace<TModel>(
        this IRuleBuilder<TModel , string> ruleBuilder , string propertyName)
        => ruleBuilder
         .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage($"The <{propertyName}> can not be <NullOrWhiteSpace>.");

    public static IRuleBuilderOptions<TModel , string> BeUserNameChars<TModel>(
     this IRuleBuilder<TModel , string> ruleBuilder , string propertyName)
     => ruleBuilder
         .BeInRange(propertyName)
         .Must(x => x.All(char.IsLetterOrDigit))
         .WithMessage($"The <{propertyName}> can only contain letters and numbers.");

    public static IRuleBuilderOptions<TModel , string> BePasswordChars<TModel>(
    this IRuleBuilder<TModel , string> ruleBuilder , string propertyName)
    => ruleBuilder
        .BeInRange(propertyName)
        .Must(x => x.Any(char.IsUpper))
        .WithMessage($"The <{propertyName}> must contain at least one uppercase letter.")
        .Must(x => x.Any(char.IsLower))
        .WithMessage($"The <{propertyName}> must contain at least one lowercase letter.")
        .Must(x => x.Any(char.IsDigit))
        .WithMessage($"The <{propertyName}> must contain at least one digit.")
        .Must(x => x.Any(char.IsPunctuation))
        .WithMessage($"The <{propertyName}> must contain at least one punctuation mark.");


    public static List<CodeMessage> AsCodeMessages(this List<ValidationFailure> validationFailures) {
        var errors = new List<CodeMessage>();
        Parallel.ForEach(validationFailures , item => {
            errors.Add(new(item.PropertyName , item.ErrorMessage));
        });
        return errors;
    }


}
