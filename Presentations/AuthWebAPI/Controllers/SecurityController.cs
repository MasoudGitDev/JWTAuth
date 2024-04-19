using Apps.Auth.Abstractions;
using Domains.Auth.AppUserEntity.Aggregate;
using Domains.Auth.AppUserEntity.Repos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.Attributes;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using Shared.Auth.ValueObjects;

namespace AuthWebAPI.Controllers;

[Authorize]
[AccountResultException]
public abstract class SecurityController(IAccountUOW _unitOfWork) : ControllerBase {

    protected async Task<AppUser> GetExistingUserByEmail(string email)
        => ( await FindByEmailAsync(email) ).ThrowIfNull($"No user found with this email: <{email}>.");

    protected async Task<AppUser?> FindByEmailAsync(string email) {
        return await _unitOfWork.Queries.FindByEmailAsync(email);
    }


    protected async Task<AppUser?> FindByIdAsync(AppUserId appUserId) {
        return await _unitOfWork.Queries.FindByIdAsync(appUserId);
    }


    protected async Task<AppUser?> FindByUserNameAsync(string userName) {
        return await _unitOfWork.Queries.FindByUserNameAsync(userName);
    }


    protected async Task<List<AppUser>> GetLockedUsersAsync() {
        return await _unitOfWork.Queries.GetLockedUsersAsync();
    }


    protected async Task<AppUser?> IsLockedBySystem(AccountLockType lockType) {
        return await _unitOfWork.Queries.IsLockedBySystem(lockType);
    }


    protected async Task<AppUser> GetUserAsync() {
        if(User is null) {
            throw new ArgumentNullException(nameof(User));
        }
        if(User.Identity is null) {
            throw new ArgumentNullException("Identity");
        }
        if(!User.Identity.IsAuthenticated) {
            throw new Exception("NotAuthenticated");
        }
        return ( await FindByUserNameAsync(User.Identity.Name ?? string.Empty) ).ThrowIfNull("UserName");
    }
    /// <summary>
    ///  Creates a link base base on client (blazor app) url.
    /// </summary>
    protected LinkModel CreateLink(
        string mainUrl = "https://localhost:7255" ,
        string controllerName = "Accounts" ,
        string actionName = "ConfirmEmail") {
        return new LinkModel($"{mainUrl}/{controllerName}/{actionName}/" + "{email}/{token}" , "{email}" , "{token}");
    }

    protected async Task<TModel> ValidateModelAsync<TValidator, TModel>(TValidator validator , TModel model)
       where TValidator : IValidator<TModel> {
        var validationResult = await validator.ValidateAsync(model);
        if(validationResult.IsValid is false) {
            throw new AccountException(validationResult.Errors.AsCodeMessages());
        }
        return model;
    }


    [HttpPost("Validate")]
    public async Task ValidateCaptchaAsync(string fileName, string userInput) {
        var captchaValue =
            HttpContext.Session.GetString(fileName)
            ?? throw new AccountException("InvalidCaptcha" , $"This <captcha> key : <{fileName}> is invalid.");
        if(!userInput.Equals(captchaValue , StringComparison.InvariantCultureIgnoreCase)) {
            throw new AccountException("InvalidCaptcha" , $"This <captcha> value : <{userInput}> is invalid.");
        }
        await Task.CompletedTask;
    }

}
