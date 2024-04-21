using Apps.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Auth.Constants.ApiAddresses;
using Shared.Auth.DTOs;
using Shared.Auth.Exceptions;
using Shared.Auth.Models;

namespace AuthWebAPI.Controllers.Utilities;
[Route("Api/[controller]")]
[ApiController]
public class CaptchaController(ICaptcha _captcha , IDistributedCache _cache) : ControllerBase {

    [HttpGet("Generate")]
    public async Task<IActionResult> GenerateCaptcha() {
        var fileName = "captcha_" + Guid.NewGuid().ToString().Replace("-", "") + ".png";
        var (Image, ImageText) = await _captcha.GenerateAsync();
        // HttpContext.Session.SetString(fileName , result.ImageText ?? "<invalid-image>");
        await _cache.SetStringAsync(fileName , ImageText);
        return new FileContentResult(Image , "image/png") { FileDownloadName = fileName };
    }


    [HttpPost("Validate")]
    public async Task<CodeMessage> ValidateCaptcha(CaptchaValidationDto model) {
        var (fileName, userInput) = (model.FileName, model.UserInput);
        var captchaValue =await _cache.GetStringAsync(fileName);
        if(String.IsNullOrWhiteSpace(captchaValue)) {
            throw new AccountException("InvalidCaptcha" , $"This <captcha> key : <{fileName}> is invalid.");
        }
        if(!userInput.Equals(captchaValue , StringComparison.InvariantCultureIgnoreCase)) {
            throw new AccountException("InvalidCaptcha" , $"This <captcha> value : <{userInput}> is invalid.");
        }
        await _cache.RemoveAsync(fileName);
        return ResultMessage.ValidCaptcha;
    }
}
