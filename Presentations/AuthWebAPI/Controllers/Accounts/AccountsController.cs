using Apps.Auth.Accounts.Commands.Models;
using AuthWebAPI.DTOs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.Models;

namespace AuthWebAPI.Controllers.Accounts;
[Route("api/[controller]")]
[ApiController]
public class AccountsController(ISender sender) : ControllerBase {

    [HttpGet("SignUp")]
    public async Task<AccountResult> SignUp([FromForm] SignUpDto model) {     
        return await sender.Send(model.Adapt<CreateAppUserModel>());
    }

    [HttpGet("SignIn")]
    public async Task SignIn() {

    }

    [HttpGet("LoginByToken")]
    public async Task LoginByToken() {

    }

}
