using Apps.Auth.Abstractions;
using Apps.Auth.Accounts.Manager;
using Apps.Auth.Services;
using Apps.Services.Services;
using Domains.Auth.AppUserEntity.Aggregate;
using Microsoft.AspNetCore.Identity;
using Shared.Auth.Enums;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;
using Shared.Auth.Models;

namespace Infra.Auth.Implements.Managers;
internal sealed class AccountManager(
    IAuthService _authService ,
    SignInManager<AppUser> _signInManager ,
    IMessageSender _messageSender ,
    IClaimsGenerator _claimsGenerator
    ) : SharedManager(_messageSender , _signInManager), IAccountManager {

    private readonly UserManager<AppUser> _userManager = _signInManager.UserManager;

    public async Task<AccountResult> LoginByTokenAsync(string authToken) {
        return await _authService.EvaluateAsync(authToken ,
            userFinder: async (userIdClaim) => ( await _userManager.FindByIdAsync(userIdClaim) )
            .ThrowIfNull("Invalid-UserId")
            .Id.ToString());
    }
    public async Task<AccountResult> SignInAsync(LoginType loginType ,
        string loginName ,
        string password ,
        bool isPersistent ,
        bool lockoutOnFailure = true) {

        var findUser = (loginType switch {
            LoginType.UserName => await _userManager.FindByNameAsync(loginName) ,
            LoginType.Email => await _userManager.FindByEmailAsync(loginName),
            _ => throw new AccountsException("Invalid-LoginType" , "The <login-type> is invalid.")}) ?? 
            throw new AccountsException("InvalidData" ,
            $"Please check [ loginType : <{loginType}>, loginName : <{loginName}> , password <***> ] again.");

        var result = await _signInManager.PasswordSignInAsync(findUser , password , isPersistent , lockoutOnFailure);
        await HandleSignInResultAsync(result , findUser , password);
        return await _authService.GenerateTokenAsync(_claimsGenerator.CreateRegularClaims(findUser.Id));
    }

    public async Task<AccountResult> RegisterAsync(AppUser appUserModel , string password , LinkModel model) {
        AppUser createUser = await CreateUserAsync(appUserModel , password);
        var result = await CreateTokenLinkAsync(createUser, model , _userManager.GenerateEmailConfirmationTokenAsync );
        await SendTokenLinkToEmailAsync(createUser.Email! , "Email-Conformation_link" , result.Link);
        return await _authService.GenerateTokenAsync(_claimsGenerator.CreateBlockClaims(createUser.Id ,
            "NotConfirmedEmail"));
    }

    #region private methods    

    private async Task<AppUser> CreateUserAsync(AppUser appUser , string password) {
        var result = await  _userManager.CreateAsync(appUser);
        if(!result.Succeeded) {
            throw new AccountsException(GetIdentityErrors(result.Errors));
        }
        result = await _userManager.AddPasswordAsync(appUser , password);
        if(!result.Succeeded) {
            throw new AccountsException(GetIdentityErrors(result.Errors));
        }
        return appUser;
    }

    private static Dictionary<string , string> GetIdentityErrors(IEnumerable<IdentityError> identityError) {
        Dictionary<string,string> errors=[];
        Parallel.ForEach(identityError , (model) => { errors.Add(model.Code , model.Description); });
        return errors;
    }

    #endregion
}
