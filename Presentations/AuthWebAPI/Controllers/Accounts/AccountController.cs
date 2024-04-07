using Apps.Auth.Abstractions;
using Apps.Auth.Accounts.Manager;
using AuthWebAPI.DTOs;
using Domains.Auth.AppUserEntity.Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.Attributes;
using Shared.Auth.Extensions;
using Shared.Auth.Models;


namespace AuthWebAPI.Controllers.Accounts;

[Route("api/[controller]")]
[ApiController]
[ResultException]
public class AccountController(IAccountUOW _unitOfWork)
    : AuthController(_unitOfWork.ThrowIfNull(nameof(IAccountUOW))) {

    private IAccountManager AccountManager => _unitOfWork.AccountManager;
    private LinkModel GetEmailConformationLink => CreateLink("ConfirmEmail" , "userId");


    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<AccountResult> RegisterAsync([FromBody] SignUpDto model) {
        var (email, userName, password, gender, birthDate) = model;
        var appUser = AppUser.Create(userName,email,gender,birthDate);
        return await AccountManager.RegisterAsync(appUser , password , GetEmailConformationLink);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<AccountResult> LoginAsync([FromBody] SignInDto model) {
        var (loginType, loginName, password, isPersistent) = model;
        return await AccountManager.LoginAsync(loginType , loginName , password , isPersistent , false);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync() {
        await AccountManager.DeleteAsync(await GetUserAsync());
        return Ok(new {
            message = "The Account has been deleted successfully"
        });
    }


}
