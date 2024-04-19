using Apps.Services.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.Constants.ApiAddresses;
using Shared.Auth.Exceptions;
using Shared.Auth.Models;

namespace AuthWebAPI.Controllers.Utilities;
[Route("Api/[controller]")]
[ApiController]
public class CaptchaController(ICaptcha _captcha) : ControllerBase {

    [HttpGet("Generate")]
    public async Task<IActionResult> GenerateCaptcha() {
        var fileName = "captcha_" + Guid.NewGuid().ToString().Replace("-", "") + ".png";
        var result = await _captcha.GenerateAsync();
        HttpContext.Session.SetString(fileName , result.ImageText ?? "<invalid-image>");
        return new FileContentResult(result.Image , "image/png") { FileDownloadName = fileName };
    }

    
    [HttpPost("Validate")]
    public CodeMessage ValidateCaptcha(string fileName , string userInput) {
        var captchaValue =
            HttpContext.Session.GetString(fileName)
            ?? throw new AccountException("InvalidCaptcha" , $"This <captcha> key : <{fileName}> is invalid.");
        if(!userInput.Equals(captchaValue , StringComparison.InvariantCultureIgnoreCase)) {
            throw new AccountException("InvalidCaptcha" , $"This <captcha> value : <{userInput}> is invalid.");
        }
        return ResultMessage.ValidCaptcha;
    }
}
