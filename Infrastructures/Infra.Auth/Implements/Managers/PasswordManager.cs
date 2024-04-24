using Apps.Services.Services;
using Apps.Services.Services.AccountManagers;
using Apps.Services.Services.Security;
using Domains.Auth.AppUserEntity.Aggregate;
using Domains.Auth.AppUserEntity.ValueObjects;
using Infra.Auth.Contexts.Write;
using Microsoft.AspNetCore.Identity;
using Shared.Auth.Exceptions;
using Shared.Auth.Models;

namespace Infra.Auth.Implements.Managers;

internal class PasswordManager(
    SignInManager<AppUser> _signInManager,
    IAuthTokenService _authService,
    IClaimsGenerator _claimsGenerator,
    IMessageSender _messageSender , 
    AppWriteDbContext _dbContext
    ) :SharedManager(_messageSender,_signInManager) , IPasswordManager
{

    private UserManager<AppUser> UserManager => _signInManager.UserManager;

    public async Task<AccountResult> ChangeAsync(AppUser appUser, string currentPassword, string newPassword)
    {

        var signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, currentPassword, false);
        if (!signInResult.Succeeded)
        {
            await HandleSignInResultAsync(signInResult , appUser , currentPassword);
        }

        var result = await UserManager.ChangePasswordAsync(appUser, currentPassword, newPassword);
        if (!result.Succeeded)
        {
            throw new AccountException("ChangePasswordError", string.Join(',', result.Errors));
        }
        return await _authService.GenerateAsync(_claimsGenerator.CreateRegularClaims(appUser.Id));
    }

    public async Task ForgotAsync(AppUser appUser, LinkModel model)
    {
        var linkModel = await CreateTokenLinkAsync(appUser,
            model,
            UserManager.GeneratePasswordResetTokenAsync,
            appUser.Email ?? "<invalid-routeId>");
        Console.WriteLine("ResetPasswordLink:\n" + linkModel.Link);
        await SendTokenLinkToEmailAsync(appUser.Email!, "PasswordResetToken", linkModel.Link);
    }

    public async Task<AccountResult> ResetAsync(AppUser appUser, string token, string newPassword)
    {        
        var result = await UserManager.ResetPasswordAsync(appUser, token, newPassword);
        if (result.Succeeded is false)
        {
            throw new AccountException("ResetPassword", string.Join(",", result.Errors));
        }
        if(appUser.PhoneNumberConfirmed) {
            // send a second conformation by sms
        }
        return await _authService.GenerateAsync(_claimsGenerator.CreateRegularClaims(appUser.Id));
    }
}
