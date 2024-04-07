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
[Authorize]
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
    [HttpPost("SignIn")]
    public async Task<AccountResult> SignInAsync([FromBody] SignInDto model) {
        var (loginType, loginName, password, isPersistent) = model;
        return await AccountManager.SignInAsync(loginType , loginName , password , isPersistent , false);
    }


    [HttpPost("LoginByToken/{token}")]
    public async Task<IActionResult> LoginByTokenAsync([FromRoute] string token) {
        return Ok(new { userId = await GetUserAsync() , token });
    }

    [AllowAnonymous]
    [HttpGet("DeviceInfo")]
    public async Task<IActionResult> GetDeviceInfo() {
        return Ok(new { deviceInfo = "Device Info"  });
    }
}
