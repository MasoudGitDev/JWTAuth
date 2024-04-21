using ClientApp.Exceptions;
using Shared.Auth.Constants.ApiAddresses;
using Shared.Auth.DTOs;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using System.Net.Http.Headers;

namespace ClientApp.Services;

public interface ICaptchaManagerService {
    Task<(string fileName, string base64Str)> GenerateAsync();
    Task<CodeMessage> ValidateAsync(CaptchaValidationDto model);
}

public class CaptchaManagerService(HttpClient _httpClient) :  ICaptchaManagerService {

    protected CancellationToken CancellationToken = new();

    public async Task<(string fileName, string base64Str)> GenerateAsync() {
        var response = await _httpClient.GetAsync(CaptchaAction.Generate ,
        HttpCompletionOption.ResponseHeadersRead ,
        CancellationToken);
        if(!response.IsSuccessStatusCode) {
            throw new HttpClientManagerException(response.StatusCode.ToString() ,
                 $"{CaptchaAction.Generate} can not load data properly!");
        }
        var responseStream = await response.Content.ReadAsStreamAsync();
        string fileName = "<invalid-file-name>";
        if(response.Content.Headers.Contains("content-disposition")) {
            var contentDisposition = ContentDispositionHeaderValue.Parse(response.Content.Headers.ContentDisposition?.ToString() ?? "");
            fileName = contentDisposition.FileNameStar ?? fileName;
        }
        using(var memoryStream = new MemoryStream()) {
            await responseStream.CopyToAsync(memoryStream);
            return (fileName, Convert.ToBase64String(memoryStream.ToArray()));
        };
    }

    public async Task<CodeMessage> ValidateAsync(CaptchaValidationDto model) {
        var response = await _httpClient.PostAsync(CaptchaAction.Validate , model.AsStringContent() , CancellationToken);
        if(!response.IsSuccessStatusCode) {
            return ResultError.InvalidCaptcha;
        }
        return (await response.Content.ReadAsStringAsync()).FromJsonToType<CodeMessage>() ?? ResultError.InvalidCaptcha;

    }
}
