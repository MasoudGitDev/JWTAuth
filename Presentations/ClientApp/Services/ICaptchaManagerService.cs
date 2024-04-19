namespace ClientApp.Services;

public interface ICaptchaManagerService {
    Task<(string fileName, string base64Str)> GenerateAsync();
}

public class CaptchaManagerService(HttpClient _httpClient) 
    : HttpClientManager(_httpClient) ,  ICaptchaManagerService {
    public async Task<(string fileName, string base64Str)> GenerateAsync() {
        return await GetCaptchaAsync();
    }
}
