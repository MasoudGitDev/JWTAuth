using Apps.Auth.Abstractions;
using Apps.Auth.Accounts.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.DTOs;
using Shared.Auth.Enums;
using Shared.Auth.Extensions;
using Shared.Auth.Models;

namespace AuthWebAPI.Controllers.Accounts;
[Route("api/[controller]")]
[ApiController]
public class PasswordController(IAccountUOW _unitOfWork)
    : AuthController(_unitOfWork.ThrowIfNull(nameof(IAccountUOW))) {

    [HttpPut("Change")]
    public async Task<AccountResult> ChangeAsync([FromBody] ChangePasswordDto model) {
        return await PasswordManager.ChangeAsync(await GetUserAsync() ,
            model.CurrentPassword ,
            model.NewPassword);
    }

    [AllowAnonymous]
    [HttpPost("Forgot/{email}")]
    public async Task<Result> ForgotAsync([FromRoute] string email) {
        var appUser = await FindByEmailAsync(email);
        if(appUser == null) {
            return new(ResultStatus.Failed , [new("NotExist" , $"This email :<{email}> not exist.")]);
        }
        await PasswordManager.ForgotAsync(appUser , GetResetPasswordLink);
        return new(ResultStatus.Succeed , [new("CheckEmail" , "Please Check your email to confirm to reset the password.")]);
    }

    [AllowAnonymous]
    [HttpPut("Reset")]
    public async Task<AccountResult> ResetAsync([FromBody] ResetPasswordDto model) {
        return await PasswordManager.ResetAsync(await GetExistingUserByEmail(model.Email) ,
            model.Token ,
            model.Password);
    }

    // ===================================== privates
    private IPasswordManager PasswordManager => _unitOfWork.PasswordManager;
    private LinkModel GetResetPasswordLink => CreateLink(actionName : "ResetPassword");  
   
}
