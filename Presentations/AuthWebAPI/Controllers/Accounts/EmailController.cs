using Apps.Services.Services;
using Apps.Services.Services.AccountManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.DTOs;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using System.Web;

namespace AuthWebAPI.Controllers.Accounts;


[Route("Api/[controller]")]
[ApiController]
public class EmailController(IAccountUOW _unitOfWork)
    : SecurityController(_unitOfWork.ThrowIfNull(nameof(IAccountUOW))) {

    private IEmailManager EmailManager => _unitOfWork.EmailManager;
    private LinkModel GetEmailConformationLink => CreateLink();

    
    [HttpPut("Confirm")]
    public async Task<AccountResult> ConfirmAsync([FromForm] EmailConfirmationDto model) {
        var appUser = await GetUserAsync();
        if(appUser.Email != model.Email) {
            throw new AccountException("InvalidRouteId" , $"This email : <{model.Email}> not belong to you.");
        }
        return await EmailManager.ConfirmAsync(await GetUserAsync() , HttpUtility.UrlDecode(model.Token));
    }

    [HttpPost("ResendEmailConformationLink")]
    public async Task<AccountResult> ResendEmailConformationLinkAsync() {

        return await EmailManager.ResendConformationLink(await GetUserAsync() , GetEmailConformationLink);
    }

}
