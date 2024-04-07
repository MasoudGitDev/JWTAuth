using Apps.Auth.Abstractions;
using Apps.Auth.Accounts.Manager;
using AuthWebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.Extensions;
using Shared.Auth.Models;

namespace AuthWebAPI.Controllers.Accounts;
[Route("api/[controller]")]
[ApiController]
public class PasswordController(IAccountUOW _unitOfWork)
    : AuthController(_unitOfWork.ThrowIfNull(nameof(IAccountUOW))) {

    private IPasswordManager PasswordManager => _unitOfWork.PasswordManager;
    private LinkModel GetResetPasswordLink => CreateLink("ConfirmResetPassword" , "newPassword");


    [HttpPost("ChangePassword")]
    public async Task<AccountResult> ChangePasswordAsync([FromBody] ChangePasswordDto model) {
        return await PasswordManager.ChangePasswordAsync(await GetUserAsync() ,
            model.CurrentPassword ,
            model.NewPassword);
    }

    [AllowAnonymous]
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPasswordAsync([FromForm] string email) {
        await PasswordManager.ForgotPasswordAsync(await GetExistingUserByEmail(email) ,
            GetResetPasswordLink);
        return Ok(new { message = "Please Check your email to confirm to reset the password." });
    }

    [AllowAnonymous]
    [HttpPost("ResetPassword")]
    public async Task<AccountResult> ResetPasswordAsync([FromBody] ResetPasswordDto model) {
        return await PasswordManager.ResetPasswordAsync(await GetExistingUserByEmail(model.Email) ,
            model.Token ,
            model.Password);
    }
}
