using Apps.Auth.Abstractions;
using Apps.Auth.Accounts.Manager;
using Apps.Auth.Services;
using Apps.Services.Services;
using Domains.Auth.AppUserEntity.Aggregate;
using Domains.Auth.AppUserEntity.ValueObjects;
using Infra.Auth.Contexts.Write;
using Microsoft.AspNetCore.Identity;
using Shared.Auth.Exceptions;
using Shared.Auth.Models;

namespace Infra.Auth.Implements.Managers;

internal class PasswordManager(
    SignInManager<AppUser> _signInManager,
    IAuthService _authService,
    IClaimsGenerator _claimsGenerator,
    IMessageSender _messageSender , 
    AppWriteDbContext _dbContext
    ) :SharedManager(_messageSender,_signInManager) , IPasswordManager
{

    private UserManager<AppUser> _userManager => _signInManager.UserManager;

    public async Task<AccountResult> ChangePasswordAsync(AppUser appUser, string currentPassword, string newPassword)
    {

        var signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, currentPassword, false);
        if (!signInResult.Succeeded)
        {
            await HandleSignInResultAsync(signInResult , appUser , currentPassword);
        }

        var result = await _userManager.ChangePasswordAsync(appUser, currentPassword, newPassword);
        if (!result.Succeeded)
        {
            throw new AccountsException("ChangePasswordError", string.Join(',', result.Errors));
        }
        return await _authService.GenerateTokenAsync(_claimsGenerator.CreateRegularClaims(appUser.Id));
    }

    public async Task ForgotPasswordAsync(AppUser appUser, LinkModel model)
    {
        if (appUser.EmailConfirmed is false)
        {
            throw new AccountsException("EmailConfirmedError", "At first , your email must be confirm.");
        }
        var linkModel = await CreateTokenLinkAsync(appUser, model, _userManager.GeneratePasswordResetTokenAsync);
        Console.WriteLine("ResetPasswordToken:\n" + linkModel.Token);
        await SendTokenLinkToEmailAsync(appUser.Email!, "PasswordResetToken", linkModel.Link);
    }

    public async Task<AccountResult> ResetPasswordAsync(AppUser appUser, string token, string newPassword)
    {        
        var result = await _userManager.ResetPasswordAsync(appUser, token, newPassword);
        if (result.Succeeded is false)
        {
            throw new AccountsException("ResetPassword", string.Join(",", result.Errors));
        }
        if(appUser.PhoneNumberConfirmed) {
            // send a second conformation by sms
        }
        return await _authService.GenerateTokenAsync(_claimsGenerator.CreateRegularClaims(appUser.Id));
    }
}
