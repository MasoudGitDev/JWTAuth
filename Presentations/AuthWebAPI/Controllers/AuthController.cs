using Apps.Auth.Abstractions;
using Apps.Services.Services;
using Domains.Auth.AppUserEntity.Aggregate;
using Domains.Auth.AppUserEntity.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.Attributes;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using Shared.Auth.ValueObjects;

namespace AuthWebAPI.Controllers;

[ResultException]
public abstract class AuthController(IAccountUOW _unitOfWork) : ControllerBase {

    protected async Task<AppUser> GetExistingUserByEmail(string email)
        => (await FindByEmailAsync(email)).ThrowIfNull($"No user found with this email: <{email}>.");

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

    protected LinkModel CreateLink(string actionName , string routeId) {
        (string routeIdPlaceholder, string tokenPlaceholder) = ("{" + routeId + "}", "{token}");
        string link =
            "https://localhost:7224" +
            $"/api/Accounts/{actionName}/" + "{"+routeId+"}/" + tokenPlaceholder;

        return new LinkModel(link , routeIdPlaceholder , tokenPlaceholder);
    }
}
