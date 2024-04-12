﻿using Apps.Auth.Abstractions;
using Apps.Auth.Accounts.Manager;
using Domains.Auth.AppUserEntity.Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.Attributes;
using Shared.Auth.DTOs;
using Shared.Auth.Enums;
using Shared.Auth.Extensions;
using Shared.Auth.Models;


namespace AuthWebAPI.Controllers.Accounts;

[Route("Api/[controller]")]
[ApiController]
[AccountResultException]
public class AccountController(IAccountUOW _unitOfWork)
    : AuthController(_unitOfWork.ThrowIfNull(nameof(IAccountUOW))) {

    private IAccountManager AccountManager => _unitOfWork.AccountManager;
    private LinkModel GetEmailConformationLink => CreateLink();


    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<AccountResult> RegisterAsync([FromBody] SignUpDto model) {
        var (email, userName, password, gender, birthDate) = model.GetValues();
        var appUser = AppUser.Create(userName,email,gender ?? Gender.Male,birthDate);
        return await AccountManager.RegisterAsync(appUser , password , GetEmailConformationLink);
    }

    [AllowAnonymous]
    [HttpPost("LoginByToken/{token}")]
    public async Task<AccountResult> LoginByTokenAsync([FromRoute] string token) {
        return await AccountManager.LoginByTokenAsync(token);
    }


    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<AccountResult> LoginAsync([FromBody] LoginDto model) {
        var (loginType, loginName, password, isPersistent) = model.GetValues();
        var result = await AccountManager.LoginAsync(loginType , loginName , password , isPersistent , false);
        return result;
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync() {
        await AccountManager.DeleteAsync(await GetUserAsync());
        return Ok(new {
            message = "The Account has been deleted successfully"
        });
    }


}
