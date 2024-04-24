using Apps.Services.Services;
using Apps.Services.Services.AccountManagers;
using Domains.Auth.AppUserEntity.Aggregate;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.Attributes;
using Shared.Auth.Constants.ApiAddresses;
using Shared.Auth.DTOs;
using Shared.Auth.Enums;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using Shared.Auth.ModelValidators;


namespace AuthWebAPI.Controllers.Accounts;

[Route("Api/[controller]")]
[ApiController]
[AccountResultException]

public class AccountController(IAccountUOW _unitOfWork)
    : SecurityController(_unitOfWork.ThrowIfNull(nameof(IAccountUOW))) {

    private IAccountManager AccountManager => _unitOfWork.AccountManager;
    private LinkModel GetEmailConformationLink => CreateLink();

    private IValidator<SignUpDto> SignUpValidator => new SignUpValidator();
    private IValidator<LoginDto> LoginValidator => new LoginValidator();

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<AccountResult> RegisterAsync([FromBody] SignUpDto model) {
        var (email, userName, password, gender, birthDate) =
            ( await ValidateModelAsync(SignUpValidator , model) ).GetValues();
        var appUser = AppUser.Create(userName,email,gender ?? Gender.Male,birthDate);
        return await AccountManager.RegisterAsync(appUser , password , GetEmailConformationLink);
    }

    [AllowAnonymous]
    [HttpPost("LoginByToken/{token}")]
    public async Task<AccountResult> LoginByTokenAsync([FromRoute] string token) {
        if(String.IsNullOrWhiteSpace(token)) {
            return new AccountResult([ResultMessage.InvalidToken]);
        }
        return await AccountManager.LoginByTokenAsync(token);
    }  

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<AccountResult> LoginAsync([FromBody] LoginDto model) {
        var (loginName, password, isPersistent) = ( await ValidateModelAsync(LoginValidator , model) ).GetValues();
        return await AccountManager.LoginAsync(loginName , password , isPersistent , false);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync() {
        await AccountManager.DeleteAsync(await GetUserAsync());
        return Ok(new {
            message = "The Account has been deleted successfully"
        });
    }


}
