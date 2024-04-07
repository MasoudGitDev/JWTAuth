using Apps.Auth.Abstractions;
using Apps.Auth.Accounts.Manager;
using AuthWebAPI.DTOs;
using Domains.Auth.AppUserEntity.Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.Exceptions;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using System.Web;

namespace AuthWebAPI.Controllers.Accounts;



[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EmailController(IAccountUOW _unitOfWork) 
    : AuthController(_unitOfWork.ThrowIfNull(nameof(IAccountUOW))) {

    private IEmailManager EmailManager => _unitOfWork.EmailManager;
    private LinkModel GetEmailConformationLink => CreateLink("ConfirmEmail" , "userId");
  
    [HttpPost("ConfirmEmail")]
    public async Task<AccountResult> ConfirmEmailAsync([FromBody] EmailConfirmationDto model) {
        Console.WriteLine("ConfirmEmailAsync is running...");
        var appUser = await GetUserAsync();
        if(appUser.Id.ToString() != model.RouteId) {
            throw new AccountsException("InvalidRouteId" , $"This routeId : <{model.RouteId}> not belong to you.");
        }
        return await EmailManager.ConfirmEmailAsync(await GetUserAsync() , HttpUtility.UrlDecode(model.Token));
    }

    [HttpPost("ResendEmailConformationLink")]
    public async Task<AccountResult> ResendEmailConformationLinkAsync() {
        return await EmailManager.ResendEmailConformationLink(await GetUserAsync() , GetEmailConformationLink);
    }

    [AllowAnonymous]
    [HttpGet("Test")]
    public IActionResult Test() {
        return Ok(new { test = "Hi Test." });
    }

}
