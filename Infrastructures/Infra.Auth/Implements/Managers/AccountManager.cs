using Apps.Auth.Abstractions;
using Apps.Auth.Abstractions.Managers;
using Apps.Auth.Services;
using Apps.Services.Services;
using Domains.Auth.AppUserEntity.Aggregate;
using Microsoft.AspNetCore.Identity;
using Shared.Auth.Enums;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using Shared.Auth.RegularExpressions;

namespace Infra.Auth.Implements.Managers;
internal sealed class AccountManager(
    IAuthTokenService _authService ,
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
    public async Task<AccountResult> LoginAsync(string loginName ,
        string password ,
        bool isPersistent ,
        bool lockoutOnFailure = true) {  
        var loginType = RegexType.Email.IsMatch(loginName) ? LoginType.Email : LoginType.UserName;
        var findUser = (loginType switch {
            LoginType.UserName => await _userManager.FindByNameAsync(loginName) ,
            LoginType.Email => await _userManager.FindByEmailAsync(loginName),
            _ => throw new AccountException("Invalid-LoginType" , "The <login-type> is invalid.")
        }) ??
            throw new AccountException("InvalidData" ,"LoginName or Password is wrong.");

        var result = await _signInManager.PasswordSignInAsync(findUser , password , isPersistent , lockoutOnFailure);
        var isEmailConfirmed = await HandleSignInResultAsync(result , findUser , password);
        return isEmailConfirmed
            ? await _authService.GenerateAsync(_claimsGenerator.CreateRegularClaims(findUser.Id , findUser.UserName!))
            : await _authService.GenerateAsync(
                _claimsGenerator.CreateBlockClaims(findUser.Id ,"NotConfirmedEmail" , findUser.UserName!) ,
                errors: [new CodeMessage("NotConfirmedEmail" , "Please Confirm your email.")]);
    }

    public async Task<AccountResult> RegisterAsync(AppUser appUserModel , string password , LinkModel model) {
        AppUser createUser = await CreateUserAsync(appUserModel , password);
        var result = await CreateTokenLinkAsync(createUser, model , _userManager.GenerateEmailConfirmationTokenAsync );
        await SendTokenLinkToEmailAsync(createUser.Email! , "Email-Conformation_link" , result.Link);
        return await _authService.GenerateAsync(
            _claimsGenerator.CreateBlockClaims(createUser.Id ,"NotConfirmedEmail" , appUserModel.UserName!) ,
            errors : [new CodeMessage("NotConfirmedEmail" , "Please Confirm your email.")]);
    }


    public async Task DeleteAsync(AppUser appUser) {
        await _userManager.DeleteAsync(appUser);
    }

    #region private methods    

    private async Task<AppUser> CreateUserAsync(AppUser appUser , string password) {
        var result = await  _userManager.CreateAsync(appUser);
        if(!result.Succeeded) {
            throw new AccountException(GetIdentityErrors(result.Errors));
        }
        result = await _userManager.AddPasswordAsync(appUser , password);
        if(!result.Succeeded) {
            throw new AccountException(GetIdentityErrors(result.Errors));
        }
        return appUser;
    }

    private static List<CodeMessage> GetIdentityErrors(IEnumerable<IdentityError> identityError) {
        List<CodeMessage> errors=[];
        Parallel.ForEach(identityError , (model) => { errors.Add(new(model.Code , model.Description)); });
        return errors;
    }

    #endregion
}
