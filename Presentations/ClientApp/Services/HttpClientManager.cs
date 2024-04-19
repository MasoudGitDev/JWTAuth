using ClientApp.Exceptions;
using Shared.Auth.Constants.ApiAddresses;
using Shared.Auth.DTOs;
using Shared.Auth.Extensions;
using System.Net.Http.Headers;

namespace ClientApp.Services;

public abstract class HttpClientManager(HttpClient _httpClient) {

    protected CancellationToken CancellationToken = new();

    protected async Task<(string fileName,string base64Str)> GetCaptchaAsync() {
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
            fileName = Path.GetFileNameWithoutExtension(contentDisposition.FileNameStar) ?? fileName;
        }
        using(var memoryStream = new MemoryStream()) {
            await responseStream.CopyToAsync(memoryStream);
            return (fileName , Convert.ToBase64String(memoryStream.ToArray()));
        };
    }

    protected async Task<AccountResultDto> PostAsync<T>(string address , T? data) {
        var response = await _httpClient.PostAsync(address,data.AsStringContent(),CancellationToken);
        return await SharedAsync(response , address);
    }

    protected async Task<TResult> PostRouteAsync<TResult>(string address , string routeId)
        where TResult : IClientResult {
        var response = await _httpClient.PostAsync($"{address}/{routeId}",null,CancellationToken);
        return await SharedAsync<TResult>(response , address);
    }

    protected async Task<AccountResultDto> DeleteAsync(string address) {
        var response = await _httpClient.DeleteAsync(address , CancellationToken );
        return await SharedAsync(response , address);
    }

    protected async Task<AccountResultDto> PutAsync<T>(string address , T data) {
        var response = await _httpClient.PutAsync(address , data.AsStringContent() , CancellationToken );
        return await SharedAsync(response , address);
    }

    protected async Task<AccountResultDto> PatchAsync<T>(string address , T data) {
        var response = await _httpClient.PatchAsync(address , data.AsStringContent() , CancellationToken );
        return await SharedAsync(response , address);
    }

    private static async Task<AccountResultDto> SharedAsync(HttpResponseMessage httpResponse , string address) {
        if(!httpResponse.IsSuccessStatusCode) {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
           throw new HttpClientManagerException(httpResponse.StatusCode.ToString() ,
                $"{address} can not load data properly! Response content: {responseContent}");
        }
        Console.WriteLine(await httpResponse.Content.ReadAsStringAsync());
        var result = ( await httpResponse.Content.ReadAsStringAsync() )
            .FromJsonToType<AccountResultDto>()
            .ThrowIfNull("The <result> can not be null.");
        return result;
    }

    protected static async Task<TResult> SharedAsync<TResult>(HttpResponseMessage httpResponse ,
        string address) where TResult : IClientResult {
        if(!httpResponse.IsSuccessStatusCode) {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            throw new HttpClientManagerException(httpResponse.StatusCode.ToString() ,
                $"{address} can not load data properly! Response content: {responseContent}");
        }
        Console.WriteLine(await httpResponse.Content.ReadAsStringAsync());
        var result = ( await httpResponse.Content.ReadAsStringAsync() )
            .FromJsonToType<TResult>()
            .ThrowIfNull("The <result> can not be null.");
        return result;
    }



}
