using Apps.Auth.Abstractions;
using Apps.Services.Services;
using Domains.Auth.AppUserEntity.Aggregate;
using Microsoft.AspNetCore.Identity;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using System.Web;

namespace Infra.Auth.Implements.Managers;
public class SharedManager(
    IMessageSender _messageSender,
    SignInManager<AppUser> _signInManager)
{

    public enum SignInError
    {
        IsNotAllowed = 1,
        IsLockedOut,
        RequiresTwoFactor,
        InvalidLoginNameOrPassword,
        NotConfirmedEmail,
        Unknown
    }

    private UserManager<AppUser> _userManager = _signInManager.UserManager;

    public async Task SendTokenLinkToEmailAsync(string email, string subject, string link)
    {
        await _messageSender.SendAsync(new()
        {
            Subject = subject,
            To = [email],
            Body = link
        });
    }

    /// <summary>
    ///  RouteId : The default value is appUserId if routeId use null value.
    /// </summary>
    public static async Task<LinkModel> CreateTokenLinkAsync(
      AppUser appUser,
      LinkModel model,
      Func<AppUser, Task<string>> tokenGenerator,
      string? routeId = null)
    {
        routeId ??= appUser.Id.ToString();
        var token = await tokenGenerator.Invoke(appUser);
        string activationLink = model.Link
            .Replace(model.RouteId, routeId)
            .Replace(model.Token, HttpUtility.UrlEncode(token));
        return new LinkModel(activationLink, routeId, token);
    }


    public async Task HandleSignInResultAsync(SignInResult result, AppUser appUser, string password)
    {   
        if (!await IsValidPassword(appUser, password))
        {
            throw new AccountsException("OnLoginNameOrPassword" ,"Your <login-name> or <password> is invalid.");
        }
        if(appUser.EmailConfirmed is false) {
            throw new AccountsException("OnConfirmedEmail" ,
                $"Your email:<{appUser.Email}> is not confirmed." +
                " The <conformation-link> has been sent to your email before.");
        }
        SignInError error = GetErrorOnSignIn(result);
        if(!result.Succeeded) {
            throw new AccountsException("SignInError" , error.ToString());
        }        
    }

    public async Task<bool> IsValidPassword(AppUser appUser, string password)
        => await _userManager.CheckPasswordAsync(appUser, password);

    private static SignInError GetErrorOnSignIn(SignInResult result)
    {
        return result switch
        {
            _ when result.IsNotAllowed => SignInError.IsNotAllowed,
            _ when result.IsLockedOut => SignInError.IsLockedOut,
            _ when result.RequiresTwoFactor => SignInError.RequiresTwoFactor,
            _ => SignInError.Unknown
        };
    }


    public static string CorrectLink(string userId, string token, LinkModel model)
    {
        return model.Link
            .Replace(model.RouteId, userId)
            .Replace(model.Token, token);
    }
}
