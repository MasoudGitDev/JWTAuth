using Apps.Auth.Abstractions;
using Apps.Auth.Accounts.Manager;
using Apps.Auth.Services;
using Apps.Services.Services;
using Domains.Auth.AppUserEntity.Aggregate;
using Microsoft.AspNetCore.Identity;
using Shared.Auth.Exceptions;
using Shared.Auth.Models;

namespace Infra.Auth.Implements.Managers;
internal sealed class EmailManager(
    SignInManager<AppUser> _signInManager ,
    IAuthService _authService ,
    IClaimsGenerator _claimsGenerator ,
    IMessageSender _messageSender
    ) : SharedManager(_messageSender , _signInManager), IEmailManager {

    private readonly UserManager<AppUser> _userManager = _signInManager.UserManager;

    public async Task<AccountResult> ChangeEmailAsync(AppUser appUser , string newEmail , string token) {
        var result = await _userManager.ChangeEmailAsync(appUser, newEmail, token);
        if(!result.Succeeded) {
            throw new AccountsException("InvalidToken" ,
                "Due to invalid token , the new email has been not confirmed.");
        }
        return await _authService.GenerateTokenAsync(_claimsGenerator.CreateRegularClaims(appUser.Id));
    }

    public async Task<AccountResult> RequestChangeEmailAsync(AppUser appUser , string newEmail , LinkModel model) {

        var token = await _userManager.GenerateChangeEmailTokenAsync(appUser, newEmail);
        string link = CorrectLink(appUser.Id.ToString(), token, model);
        await SendTokenLinkToEmailAsync(appUser.Email! , "Change-Email" , link);
        return await _authService.GenerateTokenAsync(_claimsGenerator.GetSignUpClaims(appUser.Id));
    }

    public async Task<AccountResult> ConfirmEmailAsync(AppUser appUser , string confirmationToken) {
        var result = await _userManager.ConfirmEmailAsync(appUser, confirmationToken);
        Console.WriteLine("activationLink : \n" + confirmationToken);
        if(!result.Succeeded) {
            throw new AccountsException("InvalidToken" , "The <email-confirmation-token> is invalid.");
        }
        return await _authService.GenerateTokenAsync(_claimsGenerator.CreateRegularClaims(appUser.Id));
    }



    public async Task<AccountResult> ResendEmailConformationLink(AppUser appUser , LinkModel model) {
        return await PrepareEmailConformationLinkAsync(appUser , model);
    }

    private async Task<AccountResult> PrepareEmailConformationLinkAsync(AppUser appUser , LinkModel model) {

        if(appUser.EmailConfirmed) {
            throw new AccountsException("EmailConformationToken" , "Your email has been confirmed before.");
        }
        var result = await CreateTokenLinkAsync(appUser, model , _userManager.GenerateEmailConfirmationTokenAsync );
        await SendTokenLinkToEmailAsync(appUser.Email! , "Email-Conformation_link" , result.Link);
        return await _authService.GenerateTokenAsync(_claimsGenerator.GetSignUpClaims(appUser.Id));
    }


}
